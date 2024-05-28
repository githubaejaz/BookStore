var datatable;

$(document).ready(function () {
    loadProductData();
});

function loadProductData() {
    datatable = $('#productTable').DataTable({
        ajax: '/admin/Product/getAll',
        columns: [
            { data: 'title', width: '25%' },
            { data: 'isbn', width: '15%' },
            { data: 'author', width: '25%' },
            { data: 'listPrice', width: '5%' },
            { data: 'category.name', width: '20%' },
            {
                data: 'id',
                width : '15%',
                render: function (data) {
                    var editUrl = '/admin/Product/Upsert/' + data;
                    var deleteUrl = '/admin/Product/DeleteAPI/' + data;
                    return '<div class="w-75 btn-group" role="group"> ' +
                        '<a href="' + editUrl + '" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i>Edit</a> ' +
                        '<a onClick=DeleteProduct("' + deleteUrl + '") class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i>Delete</a></div>'
                }
            }
        ]
    });
}

function DeleteProduct(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'Delete',
                success: function (data) {
                    //$('#productTable').DataTable().ajax.reload();
                    datatable.ajax.reload();
                    toastr.success(data.message);
                }
            });
        }
    });
}
