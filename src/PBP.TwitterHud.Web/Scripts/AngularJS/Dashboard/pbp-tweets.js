
angular
    .module('app')
    .factory('PBPTweets', ['$http',
        function ($http) {

            var model = {                    
                tweets: [],
                userAggregateData: {}
            };

            return {
                
                get: function (since) {
                    $http.get('/PBPTweets/Get')
                        .success(function (data) {
                            angular.copy(data, model);
                        });

                    return model;
                }
                    
            };
        }
    ]);