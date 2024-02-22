# Code Assessment

This is designed to be a simple assessment to establish your skill level and coding style. The aim is to create a file upload component in Vue that allows for the upload of an image to an Azure blob storage account and for it then to then be displayed on the same page.

The plugins and site have all been configured and tested so you should only need to alter the existing files to help speed up the assessment. 

**Front End Plugins**
 - [Vue.js (2.6.11)](https://v2.vuejs.org/)
 - [ElementUI  - Upload Component](https://element.eleme.io/#/en-US/component/upload#upload)
 - [VeeValidate](https://vee-validate.logaretm.com/v2/)
 - [Axios (0.18.0)](https://github.com/axios/axios)

## Vue Test
Build out the existing component located in `\wwwroot\js\vue-components\file-uploader.js` using the [ElementUI](https://element.eleme.io/#/en-US/component/upload#upload) upload component. 

**Requirements**
 - Restrict to images
 - Restrict to a single file
 - Max file size is 2MB

## Code Test
Update the `FilesController` to handle the file upload request. Create a service based on the `IFileUploadService` interface and implement the logic to upload to the container `dev-test-files` in Azure Storage. The storage account has been created specifically for this test so don't worry about deleting anything here. There's a helper (`AzureStorageHelper.GetContainerForDevTest()`) created to get the storage container, you'll need to pass the credentials to it from `appsettings.json` credentials provided separately.

**Requirements**
 - Check for naming conflicts when uploading to storage account 
 - Max file size is 2MB

### Notes
 - Azure Account Credentials will be provided separately for you to update `appsettings.json`
 - This is an assessment across various code technologies and style, we'll not just be looking for working code but the whole solution including validation, naming conventions, error handling, comments etc.
 - Feel free to use any other components from [ElementUI](https://element.eleme.io/#/en-US/component/installation) library to improve the upload experience.