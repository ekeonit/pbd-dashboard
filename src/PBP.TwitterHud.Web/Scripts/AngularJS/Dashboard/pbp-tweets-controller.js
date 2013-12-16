
angular
    .module('app')
    .controller('PBPTweetsController', ['$scope', 'PBPTweets',
        function ($scope, PBPTweets) {
            
            $scope.model = PBPTweets.get(moment().subtract('days', 14));

        }]);
