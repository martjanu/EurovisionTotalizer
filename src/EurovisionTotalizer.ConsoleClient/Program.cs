using EurovisionTotalizer.Domain.Core.Factories;

var eurovisionTotalizer = new EurovisionTotalizerFactory().Create();
eurovisionTotalizer.ProcessBets();