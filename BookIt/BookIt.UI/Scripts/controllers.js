var bookItControllers = angular.module("bookItControllers", []);


bookItControllers.controller('ReservationListCtrl',
  function ($scope, $http) {

      $http.get('http://localhost:55060/api/Booking/subjects').success(function(data, status, headers, config) {
          $scope.details = data;
      }).
      error(function (data, status, headers, config) {
         alert('Panic!!! Panic!!!');
      });


     // $scope.getReservations = function (){
     //         // Send an AJAX request
     //         $.getJSON($scope.uri)
     //             .done(function(data) {
     //                 // On success, 'data' contains a list of products.
     //                 $scope.details = data;
     //             alert('vavavava');
     //         });
     //};
     //$scope.getReservations();

  });

bookItControllers.controller('ReservationDetailCtrl', ['$scope', '$routeParams',
  function ($scope) {
      
  }]);


bookItControllers.controller('ReservationSearchCtrl', ['$scope', '$routeParams',
  function ($scope) {

  }]);






