using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QYChart.Model
{
    public class Access_Tocken
    {
        /*
         {"errcode":0,"errmsg":"ok","access_token":"qH8u_9pJMlQGCA7X2Q6-qgAkN9rqbJfMGIj5U-E0RnPi8uxpf3IP4i42xD9ZfKf_l5gbZP5zjH5S0a-3_agGT1Herti-DebyrF_ezCHvAFeM18NH6cZhX1Uo5auoUPL6uWlolO6iGek1iE_CYIRS0Ec4JH8dYTlrWBzkAzJfcuBbnNWU2Ycr1oqLIzSbIb_TG6mBVWHI9v3JnjSyDONA7Q","expires_in":7200}
         
         */
        public string errcode { get; set; }
        public string errmsg { get; set; }
        public string access_token { get; set; }

        public string expires_in { get; set; }

    }
}
