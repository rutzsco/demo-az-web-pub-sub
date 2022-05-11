param name string
param eventHandlerUrl string

param location string = 'eastus'
param sku string = 'Free_F1'
resource pubsub 'Microsoft.SignalRService/webPubSub@2021-09-01-preview' = {
  name: name
  location: location

  sku: {
    name: sku
    capacity: 1
  }
}

output key string = listkeys(pubsub.id, pubsub.apiVersion).primaryKey
