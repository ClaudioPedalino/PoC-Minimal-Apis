﻿namespace net6.core.Models
{
    public class GetPlaceResponse
    {
        public Guid Id { get; set; }
        public string Ciudad { get; set; }
        public string Direccion { get; set; }
        public uint Numeracion { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
    }
}
