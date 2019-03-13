#pragma checksum "D:\dev\blazory\Sandbox\Pages\Memory.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3243648c566fdab8e899229541b8f868332f3429"
// <auto-generated/>
#pragma warning disable 1591
namespace Sandbox.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Blazor;
    using Microsoft.AspNetCore.Blazor.Components;
    using System.Net.Http;
    using Microsoft.AspNetCore.Blazor.Layouts;
    using Microsoft.AspNetCore.Blazor.Routing;
    using Microsoft.JSInterop;
    using Sandbox;
    using Sandbox.Shared;
    using Domain;
    [Microsoft.AspNetCore.Blazor.Layouts.LayoutAttribute(typeof(MainLayout))]

    [Microsoft.AspNetCore.Blazor.Components.RouteAttribute("/")]
    [Microsoft.AspNetCore.Blazor.Components.RouteAttribute("/{keyword}")]
    public class Memory : Microsoft.AspNetCore.Blazor.Components.BlazorComponent
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Blazor.RenderTree.RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "card-container container");
            builder.AddContent(2, " \n");
#line 8 "D:\dev\blazory\Sandbox\Pages\Memory.cshtml"
 foreach (var card in cards)
{

#line default
#line hidden
            builder.AddContent(3, "    ");
            builder.OpenElement(4, "div");
            builder.AddAttribute(5, "class", "img-responsive" + " card" + " " + (!card.IsFlipped ? "flipped" : "") + " " + (card.IsFound ? "found" : ""));
            builder.AddAttribute(6, "onclick", Microsoft.AspNetCore.Blazor.Components.BindMethods.GetEventHandlerValue<Microsoft.AspNetCore.Blazor.UIMouseEventArgs>(() => OnClick(card)));
            builder.AddContent(7, "\n        ");
            builder.OpenElement(8, "div");
            builder.AddAttribute(9, "class", "card-front");
            builder.AddAttribute(10, "style", "background-image:" + " url(" + (card.Url) + ")");
            builder.CloseElement();
            builder.AddMarkupContent(11, "\n        <div class=\"card-back\"></div>\n    ");
            builder.CloseElement();
            builder.AddContent(12, "\n");
#line 14 "D:\dev\blazory\Sandbox\Pages\Memory.cshtml"
}

#line default
#line hidden
            builder.AddContent(13, "\n");
#line 16 "D:\dev\blazory\Sandbox\Pages\Memory.cshtml"
 if(game.State == MemoryGameState.Win)
{

#line default
#line hidden
            builder.AddContent(14, "    ");
            builder.AddMarkupContent(15, "<h1>You win!</h1>\n");
            builder.OpenElement(16, "button");
            builder.AddAttribute(17, "class", "btn btn-primary");
            builder.AddAttribute(18, "onclick", Microsoft.AspNetCore.Blazor.Components.BindMethods.GetEventHandlerValue<Microsoft.AspNetCore.Blazor.UIMouseEventArgs>(Start));
            builder.AddContent(19, "Play again!");
            builder.CloseElement();
            builder.AddContent(20, "\n");
#line 20 "D:\dev\blazory\Sandbox\Pages\Memory.cshtml"
}

#line default
#line hidden
            builder.AddContent(21, "\n");
            builder.CloseElement();
        }
        #pragma warning restore 1998
#line 24 "D:\dev\blazory\Sandbox\Pages\Memory.cshtml"
            
    [Parameter]
    protected string Keyword {get;set;}

    SearchResponse searchResponse = new SearchResponse();
    List<Card> cards= new List<Card>();
    MemoryGame game = new MemoryGame(new List<Card>(), () => offset += 8);
    static int offset = 0;
    protected override async Task OnInitAsync()
    {
        await Start();
    }

    public async Task Start(){
        cards.Clear();
        var search = Keyword;
        if(string.IsNullOrEmpty(search))
            search = "cats";

        searchResponse = await Http.GetJsonAsync<SearchResponse>($"http://api.giphy.com/v1/gifs/search?q={search}&api_key=GDFJUI7emQX0Sxy9KDREeurBI77Symzr&limit=8&offset={offset}");
        foreach(var gif in searchResponse.data){
            cards.Add(new Card {
                Url = gif.Image
                
            });
            cards.Add(new Card {
            Url = gif.Image
                
            });
        }
        Shuffle(cards);
        game = new MemoryGame(cards, () => offset += 8);
    }

    public void OnClick(Card card) {
        game.Guess(card);
    }

    Random rng = new Random();  

    public void Shuffle<T>(IList<T> list)  
    {  
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            T value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }  
    }

    public class SearchResponse
    {
        public Gif[] data { get; set; } = new Gif[]{};
    }

    public class Gif 
    {
        public string Url {get;set;}
        public Dictionary<string, Image> Images {get;set;} = new Dictionary<string, Image>{};
        public string Image => Images["fixed_height"]?.Url ?? "";
    }

    public class Image {
        public string Url {get;set;}
    }




#line default
#line hidden
        [global::Microsoft.AspNetCore.Blazor.Components.InjectAttribute] private HttpClient Http { get; set; }
    }
}
#pragma warning restore 1591
