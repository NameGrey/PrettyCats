var bookItControllers = angular.module("bookItControllers", []);


bookItControllers.controller('ReservationListCtrl',
  function ($scope, bookingService) {

      bookingService.getSubjects()
      .success(function (data, status, headers, config) {
          $scope.subjects = data;
      })
      .error(function (data, status, headers, config) {
          alert('Panic!!! Panic!!!');
      });

  });

bookItControllers.controller('ReservationDetailCtrl', 
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


bookItControllers.controller('ReservationSearchCtrl', ['$scope', '$routeParams',
  function ($scope) {

  }]);






