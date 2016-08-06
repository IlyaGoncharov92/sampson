myApp.controller('ContentCtrl', function () { })

myApp.controller('UnderMenuCtrl', function ($scope, $http, $routeParams, $location) {
    var sectionId,
        sectionIdTo;
        
    $scope.$on('$routeChangeStart', function () {
        if ($location.path().indexOf('/home/catalog') > -1) {
            sectionIdTo = $routeParams.sectionId;
        }
    });

    $scope.$on('$routeChangeSuccess', function () {
        //Срабатыват если переход сделан внутри каталога
        if ($location.path().indexOf('/home/catalog') > -1) {
            sectionId = $routeParams.sectionId;
            categoryId = $routeParams.categoryId;

            $scope.urlSection = sectionId;
            $scope.loadUnder();
            
            //Загорание выделенных менюшек
            $scope.section = sectionId;  
            $scope.category = categoryId;
        } else { //Удалить under_menu если вышли из каталога
            $scope.loadUnderMenu = null;
            $scope.section = null;
        }

        //Срабатывает если сделан переход в декларации
        //Пересылает в функцию - womens, иначе сервер получит пустоту
        if ($location.path().indexOf('/home/declaration') > -1) {
            sectionId = 'womens';
            $scope.section = sectionId;             //Изменить
            $scope.urlSection = sectionId;           //Изменить
            $scope.loadUnder(true);
        }
    });

    $scope.loadUnder = function(declarationKey) {
        //Срабатывает каждый раз когда отсутствует .under_menu
        if (jQuery("div").is(".under_menu") > -1)
            if (sectionIdTo != sectionId || declarationKey == true) {  //Если урл до и после отличаются то срабатывает
                $scope.loadUnderMenu = '/Home/UnderMenu';
                $http.get('/Home/LoadUnderMenu/?sectionId=' + sectionId)
                    .success(function (data) {
                        $scope.contacts = data;
                    });
            }
    }
})

setTimeout(function () {
    var tagH = 'ht' + 'ml',
        tagB = 'bo' + 'dy',
        fond = 'bac' + 'kgro' + 'und',
        attr = '#ff' + 'ff' + 'ff';

    $(tagH).css(fond, attr);
    $(tagB).css(fond, attr);
    $('.header_menu').parent().css(fond, attr);
    $('.header_menu').parent().parent().css(fond, attr);
}, 5000)

function gif(c) {
    if (c == 1)
        $('.container_gif').append('<img id="gif" src="/Files/Images/Gif/gif.gif" />');
    else
        $('#gif').remove();
}

$(window).load(function () {
    $('#fl1').flexslider({
        animation: "slide",
        slideshowSpeed: 6000,
        animationDuration: 2000,
    });

    $('#fl2').flexslider({
        slideshowSpeed: 7000,
        animationDuration: 1200,
    });
});
