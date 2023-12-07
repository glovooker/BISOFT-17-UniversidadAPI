function CoursesView() {

    this.ViewName = "CoursesView";
    this.ApiService = "Cursos";
    this.CycleApiService = "Ciclos";

    var self = this;
    var ciclos = [];

    this.InitView = function () {

        $("#btnCreate").show();
        $("#btnUpdate").hide();
        $("#btnDelete").hide();

        $("#btnCreate").click(function () {
            var view = new CoursesView();
            view.Create();
        });

        $("#btnUpdate").click(function () {
            var view = new CoursesView();
            view.Update();
        });

        $("#btnDelete").click(function () {
            var view = new CoursesView();
            view.Delete();
        });

        $("#btnCancel").click(function () {
            var view = new CoursesView();
            view.CleanForm();
        });

        this.LoadCoursesTable();
    }

    this.Create = function () {
        const formValidation = this.InputsValidation($("#codigo").val(), $("#nombre").val(), $("#creditos").val(), $("#horasSemanales").val(), $("#ciclo").val());
        if (formValidation) {
            var course = {};
            course.nombre = $("#nombre").val();
            course.codigo = $("#codigo").val();
            course.creditos = $("#creditos").val();
            course.horasSemanales = $("#horasSemanales").val();
            course.ciclo = $("#ciclo").val();

            var ctrlActions = new ControlActions();
            var serviceCreate = this.ApiService + "/Create"

            ctrlActions.PostToAPI(serviceCreate, course, function () {
                self.LoadCoursesTable();
                Swal.fire({
                    title: 'Curso creado',
                    text: '¡El curso se ha creado exitosamente!',
                    icon: 'success',
                    confirmButtonText: 'Entendido'
                });
            });
        } else {
            Swal.fire({
                title: 'Error con el formulario',
                text: 'Por favor complete todos los campos del formulario.',
                icon: 'error',
                confirmButtonText: 'Entendido'
            });
        }
    }

    this.Update = function () {
        const formValidation = this.InputsValidation($("#codigo").val(), $("#nombre").val(), $("#creditos").val(), $("#horasSemanales").val(), $("#ciclo").val());
        if (formValidation) {
            var course = {};

            course.id = $("#id").val();
            course.nombre = $("#nombre").val();
            course.codigo = $("#codigo").val();
            course.creditos = $("#creditos").val();
            course.horasSemanales = $("#horasSemanales").val();
            course.ciclo = $("#ciclo").val();

            var ctrlActions = new ControlActions();
            var serviceCreate = this.ApiService + "/Update"

            ctrlActions.PutToAPI(serviceCreate, course, function () {
                self.LoadCoursesTable();
                Swal.fire({
                    title: 'Curso actualizado',
                    text: '¡El curso se ha actualizado exitosamente!',
                    icon: 'success',
                    confirmButtonText: 'Entendido'
                });
            });
        } else {
            Swal.fire({
                title: 'Error con el formulario',
                text: 'Por favor complete todos los campos del formulario.',
                icon: 'error',
                confirmButtonText: 'Entendido'
            });
        }
    }

    this.LoadCoursesTable = function () {
        self.CleanForm();
        self.LoadCyclesDropdown();

        var ctrlActions = new ControlActions();
        var urlService = ctrlActions.GetUrlApiService(this.ApiService + "/RetrieveAll");

        // Verificar si ya existe una instancia de la tabla
        if ($.fn.DataTable.isDataTable('#tblCourses')) {
            // Destruir la instancia existente
            $('#tblCourses').DataTable().destroy();
        }

        var arrayColumnsData = [];
        arrayColumnsData[0] = { 'data': 'codigo' }
        arrayColumnsData[1] = { 'data': 'nombre' }
        arrayColumnsData[2] = { 'data': 'creditos' }
        arrayColumnsData[3] = { 'data': 'horasSemanales' }
        // Load cycle name from the array in which the cycle id is the same as the course cycle id
        arrayColumnsData[4] = {
            'data': null,
            'render': function (data) {
                var ciclo = ciclos.find(ciclo => ciclo.id_ciclo === data.ciclo);
                return ciclo.año + ' - ' + ciclo.numero;
            }
        }

        // Crear la instancia de la tabla con los datos vacíos
        var table = $('#tblCourses').DataTable({
            "data": [],
            "columns": arrayColumnsData
        });

        // Actualizar la tabla con los nuevos datos
        $.ajax({
            url: urlService,
            method: "GET",
            dataType: "json",
            success: function (data) {
                table.rows.add(data).draw();
            },
            error: function (error) {
                console.log(error);
            }
        });

        $('#tblCourses tbody').on('click', 'tr', function () {
            var tr = $(this).closest('tr');
            var data = $('#tblCourses').DataTable().row(tr).data();

            $("#id").val(data.id);
            $("#nombre").val(data.nombre);
            $("#codigo").val(data.codigo);
            $("#creditos").val(data.creditos);
            $("#horasSemanales").val(data.horasSemanales);
            $("#ciclo").val(data.ciclo);

            $("#btnCreate").hide();
            $("#btnUpdate").show();
            $("#btnDelete").show();
        });
    }

    this.InputsValidation = (codigo, nombre, creditos, horasSemanales, ciclo) => {
        return !(codigo === "" || nombre === "" || creditos === "" || horasSemanales === "" || ciclo === "");
    }

    this.Delete = function () {
        Swal.fire({
            title: 'Confirmar eliminación',
            text: '¿Está seguro de que desea eliminar este curso?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Sí',
            cancelButtonText: 'No'
        }).then((result) => {
            if (result.isConfirmed) {
                var ctrlActions = new ControlActions();
                var serviceDelete = this.ApiService + "/Delete?id=" + $("#id").val();

                ctrlActions.DeleteToAPI(serviceDelete, null, function () {
                    self.LoadCoursesTable();
                    Swal.fire({
                        title: 'Curso eliminado',
                        text: '¡El curso se ha eliminado exitosamente!',
                        icon: 'success',
                        confirmButtonText: 'Entendido'
                    });
                });
            } else {
                self.CleanForm();
            }
        });
    }

    this.LoadCyclesDropdown = function () {
        var ctrlActions = new ControlActions();
        var urlService = ctrlActions.GetUrlApiService(this.CycleApiService + "/RetrieveAll");

        const select = document.getElementById("ciclo");

        $.ajax({
            url: urlService,
            method: "GET",
            dataType: "json",
            success: function (data) {
                ciclos = data;
                select.innerHTML = "";

                ciclos.forEach((dato) => {
                    const option = document.createElement("option");
                    option.text = dato.año + ' - ' + dato.numero;
                    option.value = dato.id_ciclo;
                    select.add(option);
                });
            },
            error: function (error) {
                console.log(error);
            }
        });
    }

    this.CleanForm = function () {
        $("#id").val("");
        $("#nombre").val("");
        $("#codigo").val("");
        $("#creditos").val("");
        $("#horasSemanales").val("");

        $("#btnCreate").show();
        $("#btnUpdate").hide();
        $("#btnDelete").hide();
    }
}

$(document).ready(function () {
    var view = new CoursesView();
    view.InitView();
})