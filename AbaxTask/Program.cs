using AbaxTask;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Packets;
using System.Text;
using System.Text.Json;

Console.WriteLine("Test");

var factory = new MqttFactory();
var repo = new VehicleRepo();
repo.CreateTable();

using(var client = factory.CreateMqttClient())//means that it will disposed
{
    var clientOption = new MqttClientOptionsBuilder().WithTcpServer("mqtt.hsl.fi").WithKeepAlivePeriod(TimeSpan.FromSeconds(30)).Build();


    client.ApplicationMessageReceivedAsync += RecieveMessage;

    var connect = await client.ConnectAsync(clientOption);


    await client.SubscribeAsync("/hfp/v2/journey/ongoing/vp/#");

    Console.ReadLine();
    
}

async Task RecieveMessage(MqttApplicationMessageReceivedEventArgs args)
{
    var payload = args.ApplicationMessage.PayloadSegment;
    var topic = args.ApplicationMessage.Topic;
    var json = Encoding.UTF8.GetString(payload);
    var topicarray = topic.Split('/');
    var nextstop = "";
    if (topicarray.Length >= 12)
    {
        nextstop = topicarray[11];
    }

    var postion = JsonSerializer.Deserialize<VehiclePosition>(json,new JsonSerializerOptions {PropertyNameCaseInsensitive=true});
    repo.InsertData(postion.VP.Veh, postion.VP.Lat, postion.VP.Long,nextstop);
    Console.WriteLine(postion.VP.Veh);
}

/* {"VP":{"desi":"Z","dir":"1","oper":90,"veh":6304,"tst":"2024-04-13T16:38:48.608Z","tsi":1713026328,
 * "spd":43.79,"hdg":31,"lat":60.938211,"long":25.527790,"acc":0.00,"dl":-379,"odo":null,
 * "drst":null,"oday":"2024-04-13","jrn":9863,"line":286,"start":"18:35","loc":"GPS","stop":null,"route":"3001Z","occu":0}}*/