# Guide GitHub Desktop

---

## 1. Installation et connexion (une seule fois)

1. Télécharger **GitHub Desktop** : [desktop.github.com](https://desktop.github.com)
2. Ouvrir l'application → **Sign in to GitHub.com**
3. Entrer vos identifiants GitHub
4. Configurer votre nom et email quand l'application le demande

---

## 2. Récupérer le projet (une seule fois)

1. Dans GitHub Desktop : **File → Clone repository**
2. Chercher `pixel-art-game` dans la liste
3. Choisir un dossier local simple, par exemple `C:\Projets\pixel-art-game`
4. Cliquer **Clone**

> ⚠️ Choisir un chemin **sans espaces ni accents** dans le nom du dossier.

---

## 3. Se placer sur votre branche (une seule fois)

En haut au centre, cliquer sur **Current Branch**.

- Lea → choisir `gfx/lea`
- Sam → choisir `gfx/sam`
- Mia → choisir `gfx/mia`

Si votre branche n'apparaît pas : cliquer d'abord **Fetch origin** puis réessayer.

> ✅ Une fois sur votre branche, vous ne touchez qu'à vos propres fichiers. Les autres ne peuvent pas écraser votre travail.

---

## 4. Workflow quotidien

### Au début de chaque journée — récupérer les mises à jour

1. Ouvrir GitHub Desktop
2. Vérifier que vous êtes sur **votre branche** (en haut au centre)
3. Cliquer **Fetch origin**
4. Si le bouton **Pull origin** apparaît → cliquer dessus

> Ça télécharge ce que vos collègues ont envoyé depuis la veille. À faire chaque matin avant de commencer.

---

### Pendant la journée — sauvegarder votre travail

Après avoir modifié des fichiers dans Aseprite, Krita ou autre :

1. Revenir dans GitHub Desktop
2. Onglet **Changes** à gauche : vos fichiers modifiés apparaissent avec des coches
3. Cocher les fichiers que vous voulez sauvegarder (ou tout cocher)
4. En bas à gauche, remplir le champ **Summary** avec un message court :

```
art: sprites ennemis niveau 2
art: animation idle personnage
fix: correction palette couleurs HUD
```

5. Cliquer **Commit to gfx/lea** (ou votre branche)

> Le commit = une sauvegarde locale. Vous pouvez en faire plusieurs par jour.

---

### Envoyer votre travail sur le serveur (Push)

Après un ou plusieurs commits :

1. Cliquer **Push origin** (barre du haut)

> C'est maintenant visible par toute l'équipe. À faire au moins en fin de journée.

---

## 5. Signaler qu'un lot de fichiers est prêt (Pull Request)

Quand vous avez terminé un ensemble de sprites ou d'assets et qu'ils sont prêts à être intégrés dans le jeu :

1. Dans GitHub Desktop : **Branch → Create Pull Request**
2. Une page s'ouvre dans le navigateur
3. Vérifier que c'est bien : **base : develop ← compare : gfx/votre-prenom**
4. Écrire un titre clair : `Sprites ennemis niveau 2 terminés`
5. Ajouter une courte description si besoin
6. Cliquer **Create Pull Request**
7. Prévenir un dev par message qu'il y a une PR à valider

> Vous ne mergez pas vous-même. Un dev vérifie et valide.

---

## 6. Résumé — ce qu'il faut faire chaque jour

| Moment | Action dans GitHub Desktop |
|--------|---------------------------|
| Matin | Fetch origin → Pull origin si disponible |
| Après chaque session de travail | Commit (message clair) |
| Fin de journée | Push origin |
| Quand un lot est terminé | Branch → Create Pull Request |

---

## 7. Problèmes fréquents

**Mon fichier n'apparaît pas dans Changes**
→ Vérifier que le fichier est bien dans le dossier du projet (`C:\Projets\pixel-art-game\Assets\...`)

**Le bouton Push est grisé**
→ Vous n'avez pas encore fait de commit. Faire un commit d'abord.

**Il y a un conflit**
→ Ne pas toucher et prévenir un dev immédiatement.

**Je ne vois pas ma branche**
→ Cliquer Fetch origin, puis chercher à nouveau dans Current Branch.

---

## 8. Ce que vous n'avez PAS besoin de faire

- ❌ Aller sur github.com
- ❌ Utiliser un terminal ou des commandes
- ❌ Toucher aux branches `develop` ou `main`
- ❌ Merger vous-même une Pull Request
