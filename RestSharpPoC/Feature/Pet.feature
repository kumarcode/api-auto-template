Feature: Pet API
    As a user of the Pet API
    I want to be able to add and retrieve pets
    So that I can manage my pets

    Scenario: Add and retrieve a pet
        Given I add the pet using the POST Pet API
        And the pet is added successfully
        When I retrieve the pet by ID using the GET Pet API
        Then the pet is returned successfully with correct pet details