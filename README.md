# Focamap: App .NET MAUI 8 - Integração; GoogleMaps e Firebase.

## Tutorial para implementar o GoogleMaps

### 0) Crie um Novo Projeto no GoogleCloud. 
#### Acesse esse novo projeto e copie a chave que vai aparecer na janela PopUp(caso nao apareça então clique em Biblioteca e adicione o serviço de Maps para Android SDK). A partir daí é só seguir o passo-a-passo que é simples, e por fim, para ver as chaves vá no menu lateral esquerdo e clique em “Chaves e Credencias”, clique em  “EXIBIR CHAVE”.

### 1) Em seu projeto NET MAUI
#### Adicione o pacote: 
     dotnet add package Microsoft.Maui.Controls.Maps --version 8.0.60
#### Adicione a referencia em MauiProgram():
     .UseMauiMaps()
     
### 2) Em seu projeto NET MAUI vá para Platforms/Android/Android_Manifest.xml.
#### Dentro da tag <application> escreva o meta-data para referenciar a Key do Projeto criado no Google Cloud:
     <application android:allowBackup="true" android:icon="@mipmap/appicon" android:roundIcon="@mipmap/appicon_round" android:supportsRtl="true">
            <meta-data android:name="com.google.android.geo.API_KEY" android:value=“API_KEY_DO_PROJETO_GOOGLECLOUD” />
            <meta-data android:name="com.google.android.gms.version" android:value="@integer/google_play_services_version" />
   	 </application>

### 3) Ainda no arquivo Android_Manifest.xml, defina as seguintes permissões. 
#### Essas permissões garantem acesso a localização do usuário:
     	<uses-permission android:name="android.permission.ACCESS_FINE_LOCATION" />
  	  	<uses-permission android:name="android.permission.ACCESS_COARSE_LOCATION" />
     	<uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />

### 4) Vá até a pasta Resources da raiz do projeto NomeProjeto/Resources/Maps
#### Crie uma pasta onde ficará os temas do mapa. Exemplo: Resources/Maps.
     	
### 5) Abra o site que gera o tema modificado do maps: 
       https://mapstyle.withgoogle.com/
#### caso o link nao funcione, pesquise no google por algo como: 
     gerar temas google maps personalizado android json
#### 5.1 Escolha o tema, gere e copie o json.
#### 5.2 Dentro da pasta que vc criou no passo 4 gere um arquivo .json, Exemplo: night_map_style.json e Cole o tema que gerou.
#### Voce terá algo como: 
     [
       {
    "elementType": "geometry",
    "stylers": [
      {
        "color": "#242f3e"
      }
    ]
       },
       {
         "elementType": "labels.text.fill",
         "stylers": [
           {
             "color": "#746855"
           }
         ]
       },
       {
         "elementType": "labels.text.stroke",
         "stylers": [
           {
        "color": "#242f3e"
      }
    ]
       },
       {
         "featureType": "administrative.locality",
         "elementType": "labels.text.fill",
         "stylers": [
           {
             "color": "#d59563"
           }
         ]
       },
       {
         "featureType": "poi",
         "elementType": "labels.text.fill",
         "stylers": [
           {
             "color": "#d59563"
           }
         ]
       },
       {
         "featureType": "poi.park",
         "elementType": "geometry",
         "stylers": [
           {
             "color": "#263c3f"
           }
         ]
       },
       {
         "featureType": "poi.park",
         "elementType": "labels.text.fill",
         "stylers": [
           {
             "color": "#6b9a76"
           }
         ]
       },
       {
         "featureType": "road",
         "elementType": "geometry",
         "stylers": [
           {
             "color": "#38414e"
           }
         ]
       },
       {
         "featureType": "road",
         "elementType": "geometry.stroke",
         "stylers": [
           {
             "color": "#212a37"
           }
         ]
       },
       {
         "featureType": "road",
         "elementType": "labels.text.fill",
         "stylers": [
           {
             "color": "#9ca5b3"
           }
         ]
       },
       {
         "featureType": "road.highway",
         "elementType": "geometry",
         "stylers": [
           {
             "color": "#746855"
           }
         ]
       },
       {
         "featureType": "road.highway",
         "elementType": "geometry.stroke",
         "stylers": [
           {
             "color": "#1f2835"
           }
         ]
       },
       {
         "featureType": "road.highway",
         "elementType": "labels.text.fill",
         "stylers": [
           {
             "color": "#f3d19c"
           }
         ]
       },
       {
         "featureType": "transit",
         "elementType": "geometry",
         "stylers": [
           {
             "color": "#2f3948"
           }
         ]
       },
       {
         "featureType": "transit.station",
         "elementType": "labels.text.fill",
         "stylers": [
           {
             "color": "#d59563"
           }
         ]
       },
       {
         "featureType": "water",
         "elementType": "geometry",
         "stylers": [
           {
             "color": "#17263c"
           }
         ]
       },
       {
         "featureType": "water",
         "elementType": "labels.text.fill",
         "stylers": [
           {
             "color": "#515c6d"
           }
         ]
       },
       {
         "featureType": "water",
         "elementType": "labels.text.stroke",
         "stylers": [
           {
             "color": "#17263c"
           }
         ]
       }
     ]

#### 5.3 Lembre-se de definir o arquivo como EmbeddedResource.

### 6) Hora de criar a view que vai renderizar o mapa.
    Focamap.Maui/MVVM/Views/HomeMapView
    Focamap.Maui/MVVM/ViewModels/HomeMapViewModel












      



 
