# Focamap: App .NET MAUI 8 - Integração; Google Cloud, GoogleMaps API e Firebase.



https://github.com/user-attachments/assets/26aa0ce0-df11-472e-8391-918b637c4bf1



## Tutorial para implementar o GoogleMaps

### 0) Crie um Novo Projeto no GoogleCloud. 
#### Acesse esse novo projeto e copie a chave que vai aparecer na janela PopUp(caso nao apareça então clique em Biblioteca e adicione o serviço de Maps para Android SDK). A partir daí é só seguir o passo-a-passo que é simples, e por fim, para ver as chaves vá no menu lateral esquerdo e clique em “Chaves e Credencias”, clique em  “EXIBIR CHAVE”.

### 1) Em seu projeto NET MAUI
#### Adicione o pacote: 
     dotnet add package Microsoft.Maui.Controls.Maps --version 8.0.60
#### Adicione a referencia em MauiProgram():
     .UseMauiMaps()
     
### 2) Em seu projeto NET MAUI vá para Platforms/Android/Android_Manifest.xml.
#### Dentro da tag *application* escreva os meta-datas para referenciar a Key do Projeto criado no Google Cloud:  
     <meta-data android:name="com.google.android.geo.API_KEY" android:value=“API_KEY_DO_PROJETO_GOOGLECLOUD” />
     <meta-data android:name="com.google.android.gms.version" android:value="@integer/google_play_services_version" />
   	

### 3) Ainda no arquivo Android_Manifest.xml, defina as permissões abaixo. 
#### Essas permissões garantem acesso a localização do usuário:
     <uses-permission android:name="android.permission.ACCESS_FINE_LOCATION" />
     <uses-permission android:name="android.permission.ACCESS_COARSE_LOCATION" />
     <uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />

### 4) Vá até a pasta Resources da raiz do projeto NomeProjeto/Resources.
#### Crie uma pasta onde ficará os temas do mapa. Exemplo: Resources/Maps.
     	
### 5) Abra o site que gera o tema modificado do maps: 
     https://mapstyle.withgoogle.com/
#### Caso o link nao funcione, pesquise no google por algo como: 
     gerar temas google maps personalizado android json
#### 5.1 Escolha o tema, gere e copie o json.
#### 5.2 Dentro da pasta que vc criou no passo 4 gere um arquivo .json, Exemplo: night_map_style.json e Cole o tema que gerou.
#### Voce terá algo como: 
* <a href="https://github.com/Leonardogf12/Focamap.Maui/blob/main/FocamapMaui/Resources/Maps/night_map_style.json">JSON</a>
#### 5.3 Lembre-se de definir o arquivo como EmbeddedResource.

### 6) Hora de criar a view que vai renderizar o mapa.
* <a href="https://github.com/Leonardogf12/Focamap.Maui/blob/main/FocamapMaui/MVVM/Views/HomeMapView.cs">View</a>
* <a href="https://github.com/Leonardogf12/Focamap.Maui/blob/main/FocamapMaui/MVVM/ViewModels/HomeMapViewModel.cs">ViewModel</a>
* <a href="https://github.com/Leonardogf12/Focamap.Maui/blob/main/FocamapMaui/Controls/Maps/OnMapReadyCallback.cs">OnMapReadyCallback</a>












      



 
