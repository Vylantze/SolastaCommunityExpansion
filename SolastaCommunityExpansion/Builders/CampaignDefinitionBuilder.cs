﻿using System;

namespace SolastaCommunityExpansion.Builders;

public class CampaignDefinitionBuilder : DefinitionBuilder<CampaignDefinition, CampaignDefinitionBuilder>
{
    #region Constructors

    protected CampaignDefinitionBuilder(string name, Guid namespaceGuid) : base(name, namespaceGuid)
    {
    }

    protected CampaignDefinitionBuilder(string name, string definitionGuid) : base(name, definitionGuid)
    {
    }

    protected CampaignDefinitionBuilder(CampaignDefinition original, string name, Guid namespaceGuid) : base(
        original, name, namespaceGuid)
    {
    }

    protected CampaignDefinitionBuilder(CampaignDefinition original, string name, string definitionGuid) : base(
        original, name, definitionGuid)
    {
    }

    #endregion
}
