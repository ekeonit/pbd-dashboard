Feature: Dashboard
	In order to plan, track and organize social media updates
	I want to be able to see all of the company's status updates in one place

Scenario: A request is received to see all tweets since 2 weeks ago
	Given the user 'pay_by_phone' has made the following tweets
		| Text                                                                                                                                         | At               |
		| Oh the weather outside is frightful, but parking can be quite delightful! When you park and #PayByPhone, PayByPhone, PayByPhone, PayByPhone  | 2013-12-14T19:39 |
		| @socialnerdia Not yet but we're always looking to expand in the Lower Mainland. We have a pilot project w/ NYDOT in The Bronx's Little Italy | 2013-12-03T16:08 |
		| @Gingerbikeruk Yes, put it down as a car. If you're in the UK please follow @PayByPhone_UK for questions and updates. Thanks for signing up! | 2013-12-02T12:47 |
	Given the user 'PayByPhone' has made the following tweets
		| Text                                                                                                                     | At               |
		| @deblanda Hi ! you can address to @pay_by_phone who is managing North Amercia !                                          | 2013-12-13T00:29 |
		| PayByPhone et VINCI Park remportent le Grand Prix 2013 de la Revue des Collectivités Locales des paiements innovants     | 2013-12-12T13:04 |
	Given the user 'PayByPhone_UK' has made the following tweets
		| Text                                                                                                                                        | At               |
		| @Madame_Claude Its poss that you entered the incorrect unit,please email help@paybyphone.co.uk so we can look into a refund for you. Thanks | 2013-12-10T00:41 |
		| @james_coxNQE @ChelmsCouncil Apologies James however we were not aware of your issue until now,we will make sure this gets picked up ASAP.  | 2013-12-09T06:49 |
		| @ChelmsCouncil @james_coxNQE Hi James,if you could email the team help@paybyphone.co.uk your query we will look into this for you asap, tx  | 2013-12-09T06:02 |
	When a request is received to see all tweets since 2 weeks before '2013-12-15T07:41'
	Then the total number of tweets for the account 'pay_by_phone' should be 3
	And the total number of tweets for the account 'PayByPhone' should be 2
	And the total number of tweets for the account 'PayByPhone_UK' should be 3
	And the total number of times users were mentioned for the account 'pay_by_phone' should be
		| user          | number of mentions |
		| socialnerdia  | 1                  |
		| Gingerbikeruk | 1                  |
		| PayByPhone_UK | 1                  |
	And the total number of times users were mentioned for the account 'PayByPhone' should be
		| user          | number of mentions |
		| deblanda      | 1                  |
	And the total number of times users were mentioned for the account 'PayByPhone_UK' should be
		| user          | number of mentions |
		| Madame_Claude | 1                  |
		| ChelmsCouncil | 2                  |
		| james_coxNQE  | 2                  |
	And the sorted list of tweets for all of the accounts over the period should be
		| user          | text                                                                                                                                         | at               |
		| pay_by_phone  | Oh the weather outside is frightful, but parking can be quite delightful! When you park and #PayByPhone, PayByPhone, PayByPhone, PayByPhone  | 2013-12-14T19:39 |
		| PayByPhone    | @deblanda Hi ! you can address to @pay_by_phone who is managing North Amercia !                                                              | 2013-12-13T00:29 |
		| PayByPhone    | PayByPhone et VINCI Park remportent le Grand Prix 2013 de la Revue des Collectivités Locales des paiements innovants                         | 2013-12-12T13:04 |
		| PayByPhone_UK | @Madame_Claude Its poss that you entered the incorrect unit,please email help@paybyphone.co.uk so we can look into a refund for you. Thanks  | 2013-12-10T00:41 |
		| PayByPhone_UK | @james_coxNQE @ChelmsCouncil Apologies James however we were not aware of your issue until now,we will make sure this gets picked up ASAP.   | 2013-12-09T06:49 |
		| PayByPhone_UK | @ChelmsCouncil @james_coxNQE Hi James,if you could email the team help@paybyphone.co.uk your query we will look into this for you asap, tx   | 2013-12-09T06:02 |
		| pay_by_phone  | @socialnerdia Not yet but we're always looking to expand in the Lower Mainland. We have a pilot project w/ NYDOT in The Bronx's Little Italy | 2013-12-03T16:08 |
		| pay_by_phone  | @Gingerbikeruk Yes, put it down as a car. If you're in the UK please follow @PayByPhone_UK for questions and updates. Thanks for signing up! | 2013-12-02T12:47 |









