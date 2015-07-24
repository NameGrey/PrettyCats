var bookItControllers = angular.module("bookItControllers", []);


bookItControllers.controller('SubjectsCtrl',
  function ($scope, bookingService) {

      bookingService.getSubjects()
      .success(function (data, status, headers, config) {
          $scope.subjects = data;
      })
      .error(function (data, status, headers, config) {
          alert('Panic!!! Panic!!!');
      });

      $scope.filter = function() {
          bookingService.getFilteredSubjects($scope.searchCategory, $scope.searchText)
            .success(function (data, status, headers, config) {
                 $scope.subjects = data;
             })
            .error(function (data, status, headers, config) {
                alert('Panic!!! Panic!!!');
            });
      };
  });


bookItControllers.controller('OffersCtrl',
  function ($scope, bookingService) {

      bookingService.getOffers()
      .success(function (data, status, headers, config) {
          $scope.offers = data;
      })
      .error(function (data, status, headers, config) {
          alert('Panic!!! Panic!!!');
      });

  });

bookItControllers.controller('SubjectDetailsCtrl', 
  function ($scope, $routeParams, bookingService) {
      $scope.Id = $routeParams.id;
      bookingService.getSubjectDetails($scope.Id)
        .success(function (data, status, headers, config) {
            $scope.subject = data;
        })
        .error(function (data, status, headers, config) {
            alert('Panic!!! Panic!!!');
        });
  });

bookItControllers.controller('OfferDetailsCtrl',
  function ($scope, $routeParams, bookingService) {
      $scope.Id = $routeParams.id;
      bookingService.getOfferDetails($scope.Id)
        .success(function (data, status, headers, config) {
            $scope.offer = data;
        })
        .error(function (data, status, headers, config) {
            alert('Panic!!! Panic!!!');
        });
  });
