/// <reference path="~/Scripts/angular.js"/>
/// <reference path="~/Scripts/angular-mocks.js"/>
/// <reference path="~/Scripts/AngularJS/app.js"/>
/// <reference path="~/Scripts/AngularJS/Dashboard/pbp-tweets.js"/>

describe('PBPTweetsTests', function () {

    var pbpTweets,
        httpBackend;

    beforeEach(function() {
        module('app');
    });

    beforeEach(function() {

        inject(function($injector, $httpBackend) {
            pbpTweets = $injector.get('PBPTweets');
            httpBackend = $httpBackend;
        });

        httpBackend.whenGET('/PBPTweets/Get').respond({
            
            tweets: [
                {
                    text: 'A tweet to @nancy',
                    tweetedAt: '2013-02-02',
                    user: 'bob'
                },
                {
                    text: 'A tweet to @bob',
                    tweetedAt: '2013-02-03',
                    user: 'nancy'
                }
            ],

            userAggregateData: {
                "bob": {
                    totalTweets: 1,
                    mentions: {
                        "nancy": 1
                    },
                },
                "nancy": {
                    totalTweets: 1,
                    mentions: {
                        "bob": 1
                    }
                }
            }
        });
    });

    describe('a get call for tweets', function () {

        var result,
            sinceDate;

        beforeEach(function () {
            sinceDate = new Date("December 15, 2013 10:15:00");
            result = pbpTweets.get(sinceDate);
        });

        describe('before the request completes', function() {

            it('the function should return an object with no tweets', function () {
                expect(result).toEqual({
                    tweets: [],
                    userAggregateData: {}
                });
            });

        });

        describe('after the request completes', function () {

            beforeEach(function() {
                httpBackend.flush();
            });

            it('the result should have 2 tweets', function() {
                expect(result.tweets.length).toBe(2);
            });

            it('the first tweet of the result should be from bob', function() {
                expect(result.tweets[0].user).toBe('bob');
            });

            it('the first tweet of the result should have the correct text', function() {
                expect(result.tweets[0].text).toBe('A tweet to @nancy');
            });

            it('the first tweet of the result should have the correct tweeted at date time', function() {
                expect(result.tweets[0].tweetedAt).toBe('2013-02-02');
            });

            it('the second tweet of the result should be from nancy', function() {
                expect(result.tweets[1].user).toBe('nancy');
            });

            it('the second tweet of the result should have the correct text', function() {
                expect(result.tweets[1].text).toBe('A tweet to @bob');
            });

            it('the second tweet of the result should have the correct tweeted at date time', function() {
                expect(result.tweets[1].tweetedAt).toBe('2013-02-03');
            });

            it('the result should be that bob had 1 total tweets', function() {
                expect(result.userAggregateData["bob"].totalTweets).toBe(1);
            });

            it('the result should be that nancy had 1 total tweets', function() {
                expect(result.userAggregateData["nancy"].totalTweets).toBe(1);
            });

            it('the result should be that bob mentioned nancy one time', function() {
                expect(result.userAggregateData["bob"].mentions["nancy"]).toBe(1);
            });

            it('the result should be that nancy mentioned bob one time', function() {
                expect(result.userAggregateData["nancy"].mentions["bob"]).toBe(1);
            });

        });
               
    });

});