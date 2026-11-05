# NISHANA
ASP.NET Core 8 storefront in Azerbaijani with responsive catalog, category/search filters, persistent browser cart and server-validated demo orders.

## Run
```sh
dotnet build
dotnet run --urls http://127.0.0.1:5080
```
Cloud SDK: `/workspace/dotnet/dotnet`; set `DOTNET_CLI_HOME=/workspace/dotnet-home`.

Product data is in Program.cs. Sample imagery comes from Unsplash; fonts from Google Fonts. Replace these with licensed brand assets and confirmed prices before launch. The heart mark is a placeholder, not a reproduction of the supplied fingerprint logo.

Orders are stored locally in ignored App_Data/orders.jsonl. This is a development demo: no payment collection, inventory, admin authentication or customer notifications. Before public deployment add a transactional database, authenticated order management, payment provider, rate limiting, privacy/returns terms and secure backup/retention policies. Do not expose development order storage publicly.

## Render deployment
Create a Docker Web Service connected to this repository's `main` branch.
Leave Root Directory empty, set Dockerfile Path to `./Dockerfile`, and leave
Docker Command unset so the image's startup command runs. The app listens on
`0.0.0.0` using Render's `PORT` (default 10000). Use `/api/products` as the
health check path. Select **Manual Deploy → Deploy latest commit** to retry.
The public service URL appears in the Render dashboard after a successful deploy.
Local demo orders are ephemeral on Render without a persistent disk/database.
