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
  function ($scope, $route, $routeParams, bookingService) {
      if ($routeParams.id) {
          $scope.fromSubjects = true;
      }
      else {
          $scope.fromSubjects = false;
      }

      bookingService.getOffers($routeParams.id)
      .success(function (data, status, headers, config) {
          $scope.offers = data;
      })
      .error(catchServiceError);

      $scope.bookOffer = function () {
          bookingService.bookOffer($scope.Id)
            .success(function (data, status, headers, config) {
                $scope.offers.push(data);
            })
            .error(catchServiceError);
      }
  });

bookItControllers.controller('SubjectDetailsCtrl',
  function ($scope, $route, $routeParams, bookingService) {
      $scope.Id = $routeParams.id;
      $scope.newSlot = undefined;
      bookingService.getSubjectDetails($scope.Id)
        .success(function (data, status, headers, config) {
            $scope.subject = data;
        })
        .error(catchServiceError);

      bookingService.getOffers($scope.Id)
       .success(function (data, status, headers, config) {
           $scope.offers = data;
       })
       .error(catchServiceError);

      $scope.createOffer = function () {
          bookingService.createOffer($scope.Id, $scope.newSlot.StartDate, $scope.newSlot.EndDate)
            .success(function (data, status, headers, config) {
                $scope.offers.push(data);
            })
            .error(catchServiceError);
      }

      $scope.bookOffer = function (offerId, slot) {
          bookingService.bookOffer(offerId, slot.StartDate, slot.EndDate)
            .success(function (data, status, headers, config) {
                $scope.offers.put(data);
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

      $scope.bookOffer = function (offerId, slot) {
          bookingService.bookOffer(offerId, slot.StartDate, slot.EndDate)
            .success(function (data, status, headers, config) {
                $scope.offer = data;
            })
            .error(catchServiceError);
      }
  });

catchServiceError = function (data, status, headers, config) {
    alert(data);
}




