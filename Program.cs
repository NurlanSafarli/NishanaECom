using System.Text.Json;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseDefaultFiles(); app.UseStaticFiles();
var products = new [] {
 new Product(1,"Yonca zərifliyi","Qolbaq",32,"photo-1611591437281-460bfbe1220a"),
 new Product(2,"Qızıl toxunuş","Üzük",25,"photo-1605100804763-247f67b3557e"),
 new Product(3,"İşığın ahəngi","Boyunbağı",38,"photo-1599643477877-530eb83abc8e"),
 new Product(4,"Sənin ulduzun","Sırğa",28,"photo-1630019852942-f89202989a4d"),
 new Product(5,"Zamansız anlar","Saat",65,"photo-1524805444758-089113d48a6d"),
 new Product(6,"İncə bir nişanə","Dəst",49,"photo-1617038220319-276d3cfab638")};
app.MapGet("/api/products", () => products);
var gate = new SemaphoreSlim(1);
app.MapPost("/api/orders", async (Order order) => {
 if(string.IsNullOrWhiteSpace(order.Name)||order.Name.Length>100||string.IsNullOrWhiteSpace(order.Phone)||order.Phone.Length>30||string.IsNullOrWhiteSpace(order.Address)||order.Address.Length>500||order.Items is null||order.Items.Length is 0 or >30||order.Items.Any(i=>i.Quantity is <1 or >20||!products.Any(p=>p.Id==i.ProductId))) return Results.BadRequest(new {message="Sifariş məlumatlarını yoxlayın."});
 var id=Guid.NewGuid().ToString("N"); var total=order.Items.Sum(i=>products.Single(p=>p.Id==i.ProductId).Price*i.Quantity);
 await gate.WaitAsync(); try {Directory.CreateDirectory("App_Data"); await File.AppendAllTextAsync("App_Data/orders.jsonl",JsonSerializer.Serialize(new {id,created=DateTimeOffset.UtcNow,order,total})+"\n");} finally {gate.Release();}
 return Results.Ok(new {id,total});
});
app.Run();
record Product(int Id,string Name,string Category,decimal Price,string Image);
record Item(int ProductId,int Quantity);
record Order(string Name,string Phone,string Address,Item[] Items);
