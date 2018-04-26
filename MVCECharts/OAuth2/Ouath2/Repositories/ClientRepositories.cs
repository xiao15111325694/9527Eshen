﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OAuth2.Ouath2.Models;

namespace OAuth2.Ouath2.Repositories
{
    public class ClientRepository
    {
        public static List<Client> Clients = new List<Client>() {
            new Client{
                Id = "test1",
                RedirectUrl = "http://localhost:59273/",
                Secret = "123456789"
            },
            new Client{
                Id = "test2",
                RedirectUrl = "http://XXX.XXX.XXX/",
                Secret = "987654321"
            }
        };
    }
}