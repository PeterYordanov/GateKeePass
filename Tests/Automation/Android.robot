*** Settings ***
Library  AppiumLibrary

*** Variables ***
${SERVER_URL}       http://localhost:4723/wd/hub
${PLATFORM_NAME}    Android
${DEVICE_NAME}      YourAndroidDeviceName
${APP_PATH}         path_to_your_MAUI_app.apk

*** Test Cases ***
Open MAUI App And Click Button
    Open Application    ${SERVER_URL}    platformName=${PLATFORM_NAME}    deviceName=${DEVICE_NAME}    app=${APP_PATH}
    Wait Until Page Contains Element    id=btn_click_me    30s
    Click Element    id=btn_click_me
    # You can add more steps to validate UI changes or other behaviors after button click.
    Close Application

*** Keywords ***
# You can define custom keywords here, if needed.
