Feature: Pagination3

	As a visitor to the classified website
	I want to be able to browse long lists of adverts
	page by page.

Scenario: Pagination test with 25 records
	Given 25 adverts in a list and a page size of 10
	When I view the list
	Then only 10 items should be visible
	And the pager should have 3 pages
	And I should be on page 1
	When I click page 2 link
	Then I should be on page 2
	When I click 30 items per page
	Then there should only be 1 page