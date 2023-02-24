# Projet Météo
Ce projet a pour but de créer un application météo avec glade à partir d'openwethermap. Voici une explication de l'application ainsi que de son lancement.

<br />

# Sommaire
- I - Lancement
- II - Onglet weather
- III - Onglet Forecast
- IV - Onglet Settings

<br />

# I - Lancement
Tout d'abord, il faut taper la commande `` dotnet run`` dans la console de votre terminal vsCode. Ensuite, votre clé API openwethermap vous sera demandée, il faudra la donner  **sans erreurs** (espaces, caractères en trop, etc). Elle sera enregistrée automatiquement et vous n'aurez plus besoin de la rentrer à nouveau. Si vous n'en avez pas, il faudra vous en créer une sur le site "https://openweathermap.org/". Une fois fait, une fenêtre glade s'ouvrira et vous pourrez donc utiliser l'application.

<br />

# II - Onglet weather
La clé API donnée, vous avez accès automatiquement à l'application. On découvre qu'il y a trois onglets, dont le premier qui est l'onglet "Weather", ce n'est autre que la page d'accueil qui affiche la météo actuelle et pour l'instant de la ville par défaut enregistrée. On pourra changer cette ville par défaut un peu plus tard.

<br />

Dans cet onglet il y a donc toutes les informations concernant la ville par défaut au centre de la page : le nom de la ville, la température qu'il y fait, l'humidité dans l'air, une courte description du temps, une image associée à ce temps et enfin la latitude et la longitude de la ville en-bas à droite de l'application. Petite particularité, la température est un bouton sur lequel vous pouvez appuyer pour passer la température de Celsius à Fahrenheit ou inversement. Ce changement s'effectue également dans l'onglet Forecast que l'on verra juste après.

<br />

Pour finir sur cet onglet, l'option la plus importante : La barre de recherche.

Il suffit de rentrer la ville dont vous voulez connaître la météo et appuyer sur le bouton "Search" pour que les informations sur la ville que vous cherchez s'affichent.

<br />

# III - Onglet Forecast
L'onglet "Forecast" n'est autre que l'onglet des prévisions. Il faut savoir que cet onglet est en lien direct avec l'onglet Weather. Si dans Weather vous mettez la météo de Paris, alors dans Forecast vous aurez la prévision de Paris sur les 5 prochains jours. De même pour la ville par défaut quand vous rentrez dans l'application, vous aurez les prévisions de la ville par défaut.

<br />

Cet onglet est coupé en 5 parties, chaque partie est la une prévision de plus en plus éloignée dans le temps allant jusqu'à 5 jour. Chaque prévision est prise à 12h00. Vous retrouverez exactement les mêmes informations que dans l'onglet Weather mise à part les coordonnées de la ville qui eux sont déjà dans Weather et ne changent pas.

<br />

# IV - Onglet Settings
Enfin, voici le dernier onglet : l'onglet "Settings".

C'est ici que vous pourrez changer la ville par défaut grâce à la barre de recherche et au bouton "Confirm" pour appliquer le changement. Vous pourrez même supprimer la ville par défaut et en mettre aucune avec le bouton "Reset".