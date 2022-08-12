using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.ShareDocumentUsingRESTAPI
{
    public class ShareDocumentResponse
    {
        public string odatametadata { get; set; }
        public Sharinglinkinfo sharingLinkInfo { get; set; }
    }

    public class Sharinglinkinfo
    {
        public bool AllowsAnonymousAccess { get; set; }
        public object ApplicationId { get; set; }
        public bool BlocksDownload { get; set; }
        public DateTime Created { get; set; }
        public Createdby CreatedBy { get; set; }
        public object Description { get; set; }
        public bool Embeddable { get; set; }
        public string Expiration { get; set; }
        public bool HasExternalGuestInvitees { get; set; }
        public Invitation[] Invitations { get; set; }
        public bool IsActive { get; set; }
        public bool IsAddressBarLink { get; set; }
        public bool IsCreateOnlyLink { get; set; }
        public bool IsDefault { get; set; }
        public bool IsEditLink { get; set; }
        public bool IsFormsLink { get; set; }
        public bool IsManageListLink { get; set; }
        public bool IsReviewLink { get; set; }
        public bool IsUnhealthy { get; set; }
        public DateTime LastModified { get; set; }
        public Lastmodifiedby LastModifiedBy { get; set; }
        public bool LimitUseToApplication { get; set; }
        public int LinkKind { get; set; }
        public string PasswordLastModified { get; set; }
        public object PasswordLastModifiedBy { get; set; }
        public object[] RedeemedUsers { get; set; }
        public bool RequiresPassword { get; set; }
        public bool RestrictedShareMembership { get; set; }
        public int Scope { get; set; }
        public string ShareId { get; set; }
        public string ShareTokenString { get; set; }
        public int SharingLinkStatus { get; set; }
        public bool TrackLinkUsers { get; set; }
        public string Url { get; set; }
    }

    public class Createdby
    {
        public string email { get; set; }
        public object expiration { get; set; }
        public int id { get; set; }
        public bool isActive { get; set; }
        public bool isExternal { get; set; }
        public object jobTitle { get; set; }
        public string loginName { get; set; }
        public string name { get; set; }
        public int principalType { get; set; }
        public object userId { get; set; }
        public object userPrincipalName { get; set; }
    }

    public class Lastmodifiedby
    {
        public string email { get; set; }
        public object expiration { get; set; }
        public int id { get; set; }
        public bool isActive { get; set; }
        public bool isExternal { get; set; }
        public object jobTitle { get; set; }
        public string loginName { get; set; }
        public string name { get; set; }
        public int principalType { get; set; }
        public object userId { get; set; }
        public object userPrincipalName { get; set; }
    }

    public class Invitation
    {
        public Invitedby invitedBy { get; set; }
        public DateTime invitedOn { get; set; }
        public Invitee invitee { get; set; }
    }

    public class Invitedby
    {
        public string email { get; set; }
        public object expiration { get; set; }
        public int id { get; set; }
        public bool isActive { get; set; }
        public bool isExternal { get; set; }
        public object jobTitle { get; set; }
        public string loginName { get; set; }
        public string name { get; set; }
        public int principalType { get; set; }
        public object userId { get; set; }
        public object userPrincipalName { get; set; }
    }

    public class Invitee
    {
        public string email { get; set; }
        public object expiration { get; set; }
        public int id { get; set; }
        public bool isActive { get; set; }
        public bool isExternal { get; set; }
        public object jobTitle { get; set; }
        public string loginName { get; set; }
        public string name { get; set; }
        public int principalType { get; set; }
        public object userId { get; set; }
        public string userPrincipalName { get; set; }
    }
}
