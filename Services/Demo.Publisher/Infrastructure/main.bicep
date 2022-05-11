@description('The function application storage account name.')
param functionAppStorageAccountName string

@description('The environment prefix to append to resource names.')
param functionAppName string

@description('The environment prefix to append to resource names.')
param location string = 'eastus'

// Web PubSub
module webpubsub 'pubsub.bicep' = {
  name: 'WebPubSubDeploy'
  params: {
    name: functionAppName
    eventHandlerUrl: 'TBD'
    location: location
  }
}

// Function
module function 'function.bicep' = {
  name: 'FunctionDeploy'
  params: {
    function_app_name: functionAppName
    storage_account_name: functionAppStorageAccountName
    location: location
  }
}
