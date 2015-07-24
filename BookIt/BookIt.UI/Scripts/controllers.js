var bookItControllers = angular.module("bookItControllers", []);


bookItControllers.controller('SubjectsCtrl',
  function ($scope, bookingService) {

      bookingService.getSubjects()
      .success(function (data, status, headers, config) {
          $scope.subjects = data;
      })
      .error(catchServiceError);

  });


bookItControllers.controller('OffersCtrl',
  function ($scope, bookingService) {

      bookingService.getOffers()
      .success(function (data, status, headers, config) {
          $scope.offers = data;
      })
      .error(catchServiceError);

  });

bookItControllers.controller('SubjectDetailsCtrl',
  function ($scope, $routeParams, bookingService) {
      $scope.Id = $routeParams.id;
      bookingService.getSubjectDetails($scope.Id)
        .success(function (data, status, headers, config) {
            $scope.subject = data;
        })
        .error(catchServiceError);

      $scope.createOffer = function () {
          bookingService.createOffer($scope.Id)
            .success(function (data, status, headers, config) {
                $scope.subject = data;
            })
            .error(catchServiceError);
      }

  });

bookItControllers.controller('OfferDetailsCtrl',
  function ($scope, $routeParams, bookingService) {
      $scope.Id = $routeParams.id;
      bookingService.getOfferDetails($scope.Id)
        .success(function (data, status, headers, config) {
            $scope.offer = data;
        })
        .error(catchServiceError);
  });

catchServiceError = function (data, status, headers, config) {
    alert(data);
}




