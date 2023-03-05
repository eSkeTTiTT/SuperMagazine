function putIntoBucket(id, name, price, imageUrl) {
    var formData = new FormData();

    formData.append("id", id);
    formData.append("name", name);
    formData.append("price", price);
    formData.append("imageUrl", imageUrl);

    $.ajax({
        method: 'Post',
        data: formData,
        url: '/Catalog/PutIntoBucket',
        processData: false,
        contentType: false,
        success: function (result) {

        },
        error: function (xhr, status, p3, p4) {

        }
    });
}