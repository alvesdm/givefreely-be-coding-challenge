Feature: AffiliateController

Affiliate controller endpoints

Scenario: Insert an Affiliate
	When I POST an Affiliate to '/api/v1/Affiliate'
	Then the response status code is 201
	And and the response has the resource uri in the Location header

Scenario: Fetch an Affiliate
	Given an Affiliate has just been inserted
	When I try to fetch it from '/api/v1/Affiliate/{uniqueid}'
	Then the response status code is 200
	And and the response is a an Affiliate with the same UniqueId
