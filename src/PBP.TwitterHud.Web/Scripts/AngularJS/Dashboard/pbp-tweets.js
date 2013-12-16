
angular
    .module('app')
    .factory('PBPTweets', ['$http',
        function ($http) {

            var model = {                    
                tweets: [],
                userAggregateData: {}
            };

            return {
                
                get: function (sinceDateTime) {
                    $http({
                        method: 'GET',
                        url: '/PBPTweets/Get',
                        params: {
                            sinceDateTime: sinceDateTime.toJSON()
                        }
                    })
                        .success(function (data) {
                            angular.copy(data, model);
                        });

                    return model;
                }
                    
            };
        }
    ]);