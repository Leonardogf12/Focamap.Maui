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



 
