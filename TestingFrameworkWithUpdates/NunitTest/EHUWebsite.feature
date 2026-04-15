Feature: EHU Website End-to-End User Journey
  As a user
  I want to navigate the site, search for information, and view contact details
  So that I can verify the website works correctly

  @navigation
  Scenario Outline: User navigates through main menu sections
    Given I open the home page
    When I click the navigation link "<LinkText>"
    Then the page URL should contain "<UrlPart>"
    And the page title should contain "<TitlePart>"
    And the page header should contain "<TitlePart>"

    Examples:
      | LinkText   | UrlPart      | TitlePart   |
      | About      | /about       | About       |
      | Admissions | /admissions  | Admissions  |

  @search
  Scenario Outline: User performs a search query
    Given I open the home page
    When I search for "<Query>"
    Then the search results URL should be "<ExpectedUrl>"

    Examples:
      | Query          | ExpectedUrl                                      |
      | study programs | https://en.ehuniversity.lt/?s=study+programs     |
      | Samal          | https://en.ehuniversity.lt/?s=Samal              |
      | Rector         | https://en.ehuniversity.lt/?s=Rector             |

  @contact
  Scenario: User views contact information
    Given I open the contact page
    Then the contact list should contain at least 4 items
    And the email should contain "franciskscarynacr@gmail.com"
    And the phone for country "LT" should contain "+370 68 771365"
    And the phone for country "BY" should contain "+375 29 5781488"
    And the social block should contain:
      | Facebook |
      | Telegram |
      | VK       |

  @language
  Scenario: User changes the website language
    Given I open the home page
    When I change language to "Lithuanian"
    Then the page should reload in Lithuanian
