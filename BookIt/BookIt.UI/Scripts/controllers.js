var bookItControllers = angular.module("bookItControllers", []);

bookItControllers.controller('ReservationListCtrl', ['$scope', '$routeParams',
  function ($scope) {

      var uri = '/api/BookingEntities/GetBookingEntities';
      getReservations();

      function getReservations() {
              // Send an AJAX request
              $.getJSON(uri)
                  .done(function(data) {
                      // On success, 'data' contains a list of products.
                      $scope.details = data;
                  alert('vavavava');
              });
          };
  }]);

bookItControllers.controller('ReservationDetailCtrl', ['$scope', '$routeParams',
  function ($scope) {
      
  }]);


bookItControllers.controller('ReservationSearchCtrl', ['$scope', '$routeParams',
  function ($scope) {

  }]);






