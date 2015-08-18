var bookItControllers = angular.module("bookItControllers", []);


bookItControllers.controller('SubjectsCtrl',
  function ($scope, bookingService) {
      bookingService.getSubjects()
      .success(function (data, status, headers, config) {
          $scope.subjects = data;
      })
      .error(catchServiceError);
  });

bookItControllers.controller('CategoriesCtrl',
  function ($scope, bookingService) {
  	bookingService.getCategories()
	.success(function (data, status, headers, config) {
		$scope.categories = data;
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

      var populateData = function () {
          bookingService.getOffers($routeParams.id)
            .success(function (data, status, headers, config) {
                $scope.offers = data;
            })
      .error(catchServiceError);
      }

      populateData();

      $scope.bookOffer = function (offerId, slot) {
          bookingService.bookOffer(offerId, slot.StartDate, slot.EndDate)
            .success(function (data, status, headers, config) {
                populateData();
            })
            .error(catchServiceError);
      }

      $scope.unBookOffer = function (slot) {
          bookingService.unBookOffer(slot)
            .success(function (data, status, headers, config) {
                populateData();
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

      var populateData = function () {
          bookingService.getOffers($scope.Id)
            .success(function (data, status, headers, config) {
                $scope.offers = data;
            })
      .error(catchServiceError);
      }

      populateData();


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
                populateData();
            })
            .error(catchServiceError);
      }

      $scope.unBookOffer = function (slot) {
          bookingService.unBookOffer(slot)
            .success(function (data, status, headers, config) {
                populateData();
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

      $scope.unBookOffer = function (slot) {
          bookingService.unBookOffer(slot)
            .success(function (data, status, headers, config) {
                $scope.offer = data;
            })
            .error(catchServiceError);
      }
  });

catchServiceError = function (data, status, headers, config) {
    alert("Service error: " + data.Message + "\nStackTrace:" + data.StackTrace);
}




