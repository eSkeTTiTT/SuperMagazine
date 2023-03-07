function removeItemFromBucket(id) {
    var formData = new FormData();

    formData.append("id", id);

    $.ajax({
        method: 'Post',
        data: formData,
        url: '/Bucket/RemoveItemFromBucket',
        processData: false,
        contentType: false,
        success: function (data) {
            document.body.innerHTML = data;
        },
        error: function (xhr, status, p3, p4) {

        }
    });
}

function putItemIntoBucket(id, name, price, imageUrl) {
    var formData = new FormData();

    formData.append("id", id);
    formData.append("name", name);
    formData.append("price", price);
    formData.append("imageUrl", imageUrl);

    $.ajax({
        method: 'Post',
        data: formData,
        url: '/Bucket/PutItemIntoBucket',
        processData: false,
        contentType: false,
        success: function (data) {
            $("#bucket").text("Корзина (" + data.count + ")");
            alert("Товар добавлен в корзину");
        },
        error: function (xhr, status, p3, p4) {

        }
    });
}