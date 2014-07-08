using System.Web.Http;
using Thinktecture.IdentityModel.Http.Cors.WebApi;

public class CorsConfig {

    public static void RegisterCors(HttpConfiguration httpConfig) {

        var corsConfig = new WebApiCorsConfiguration();



        // this adds the CorsMessageHandler to the HttpConfiguration's

        // MessageHandlers collection



        corsConfig.RegisterGlobal(httpConfig);



        // this allows all CORS requests to the RestfulObjects controller

        // from the http://foo.com origin.

        corsConfig.ForResources("RestfulObjects").ForOrigins("http://localhost:49998").AllowAll();



    }

}