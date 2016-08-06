
myApp.controller('ManageCtrl', function ($scope, $http) {
    var wrapKey = true;
    $scope.gifVisible = false;
    
    $http.get('/Manage/LoadUserImages')
        .success(function (data) {
        if (data == "") {
            $scope.imagePath = 'PersonalPhoto.png';
        }
        else {
            $scope.imagePath = data[0].ImagePath;
            wrapKey = false;
            $scope.wrapEvents();
        }
    })
    
    $scope.wrapEvents = function () {
        if (wrapKey == true) {
            $(".displayed").css({
                'pointer-events': 'all'
            });
        } else {
            $(".displayed").css({
                'pointer-events': 'none'
            });
        }
    }

    $scope.wrapVisible = function (e) {
        if (wrapKey == true) {
            $scope.wrapShow = false;
            if (e.type == 'mouseenter')
                $scope.wrapShow = true;
        }
    }

    $('.displayed').on('click', function () {
        $('.img_file')[0].click();
    });

    $(".img_file").on('change', function Send(event) {
        $scope.gifVisible = true;
        var files = this.files;
        if (files.length == 1) {
            if (window.FormData !== undefined) {
                var imgData = new FormData();
                imgData.append("file", files[0]);
                $.ajax({
                    type: "POST",
                    url: '/Manage/UploadImage',
                    contentType: false,
                    processData: false,
                    data: imgData,
                    success: function (data) {
                        $scope.imagePath = data;
                        $scope.gifVisible = false;
                        wrapKey = false;
                        $scope.wrapEvents();
                        $scope.$apply();
                    },
                    error: function (xhr, status, p3) {
                        alert(xhr.responseText);
                    }
                });
            }
            else {
                alert("Браузер не поддерживает загрузку файлов HTML5!");
            }
        }
    });
})

