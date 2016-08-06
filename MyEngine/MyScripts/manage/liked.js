
myApp.controller('ManageLikedCtrl', function ($scope, $http) {
    $http.get('/Manage/LikedDeclaration')
        .success(function (data) {
            if (data == '') {
                $scope.likedVisible = true;

            } else {
                $scope.likedVisible = false;
                $scope.answerTrue(data);
            }
        })

    $scope.answerTrue = function (dataDeclaration) {
        $http.get('/Manage/LikedDeclarationImages')
            .success(function (data) {
                $scope.imagePaths = data;
                $scope.declarations = dataDeclaration;
            })
    }

    $scope.likedDelete = function (declaration) {
        var index = $scope.declarations.indexOf(declaration);
        $scope.declarations.splice(index, 1);

        if ($scope.declarations == '')
            $scope.likedVisible = true;

        $http.get('/Manage/DeleteLikedDeclaration/?idDeclaration=' + declaration.Id)
               .success(function (data) { })
    }
})