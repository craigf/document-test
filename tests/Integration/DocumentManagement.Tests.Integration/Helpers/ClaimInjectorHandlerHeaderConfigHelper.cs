using System.Security.Claims;
using Newtonsoft.Json;

namespace DocumentManagement.Tests.Integration.Helpers
{
    public class ClaimInjectorHandlerHeaderConfigHelper
    {
        [JsonProperty]
        private readonly Dictionary<string, List<string>> _claims = new Dictionary<string, List<string>>();

        [JsonConstructor]
        internal ClaimInjectorHandlerHeaderConfigHelper() => Reset();

        /// <summary>
        /// True makes the client generate an anonymous request, false causes a client
        /// with all claims added to be generated.
        /// </summary>
        [JsonProperty]
        public bool AnonymousRequest { get; set; }

        /// <summary>
        /// Sets the name claim.
        /// </summary>
        public string Name
        {
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));

                _claims.Remove(ClaimTypes.Name);

                _claims.Add(ClaimTypes.Name, new List<string> { value });
            }
        }

        /// <summary>
        /// Clears existing roles and sets the current roles to the array passed in.
        /// </summary>
        public string[] Roles
        {
            set
            {
                if (value == null || value.Any(x => x == null))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                _claims.Remove(ClaimTypes.Role);

                foreach (var role in value)
                {
                    AddClaim(ClaimTypes.Role, role);
                }
            }
        }

        /// <summary>
        /// Adds a claim to the collection of claims.
        /// </summary>
        /// <param name="claimType">The type of claim. <see cref="ClaimTypes"/> for standard values.</param>
        /// <param name="value">The value of the claim.</param>
        public void AddClaim(string claimType, string value)
        {
            if (!_claims.TryGetValue(claimType, out var values))
            {
                values = new List<string>();
                _claims.Add(claimType, values);
            }

            values.Add(value);
        }

        /// <summary>
        /// Adds a claim to the collection of claims.
        /// </summary>
        /// <param name="claim">The claim to add.</param>
        public void AddClaim(Claim claim) => AddClaim(claim.Type, claim.Value);

        internal IEnumerable<Claim> Claims => _claims.SelectMany(x => x.Value.Select(y => new Claim(x.Key, y)));

        /// <summary>
        /// Resets the claim collection to it's default state.
        /// </summary>
        public void Reset()
        {
            _claims.Clear();
            AnonymousRequest = false;
            Name = "Authenticated User";
            Roles = new string[0];
        }
    }
}
