using System.Text.Json.Serialization;

namespace NextLevelDev.Keycloak.Models.PermissionTicket;

internal sealed record GetPermissionTicketResponseJsonData([property: JsonPropertyName("ticket")] string Ticket);
