using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenerateReview.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Markov;

namespace GenerateReview
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            string path = AppDomain.CurrentDomain.BaseDirectory.ToString() + "Luxury_Beauty_5.json";
            ReviewList.sentence = new List<string>();

            using (var jsonRead = new StreamReader(path))
            {
                var chain = new MarkovChain<string>(1);
                string jsonLine;
                string value;
                while ((jsonLine = jsonRead.ReadLine()) != null)
                {
                    Dictionary<string, object> dicJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonLine);
                    if (dicJson.ContainsKey("reviewText"))
                    {
                        value = dicJson["reviewText"].ToString();
                        string[] words = value.Split();
                        chain.Add(words, 1);
                    }
                }
                var rand = new Random();

                for (int i = 0; i < 5; i++)
                {
                    var markovSentence = string.Join(" ", chain.Chain(rand));
                    ReviewList.sentence.Add(markovSentence);
                }
            }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GenerateReview", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GenerateReview v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
