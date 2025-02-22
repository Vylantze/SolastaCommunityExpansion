﻿using System;
using System.Collections.Generic;
using SolastaCommunityExpansion.CustomDefinitions;

namespace SolastaCommunityExpansion.Builders.Features;

public class IgnoreDynamicVisionImpairmentBuilder : FeatureDefinitionBuilder<
    FeatureDefinitionIgnoreDynamicVisionImpairment,
    IgnoreDynamicVisionImpairmentBuilder>
{
    public IgnoreDynamicVisionImpairmentBuilder SetMaxRange(float range)
    {
        Definition.maxRange = range;
        return this;
    }

    public IgnoreDynamicVisionImpairmentBuilder AddRequiredFeatures(params FeatureDefinition[] features)
    {
        Definition.requiredFeatures.AddRange(features);
        return this;
    }

    public IgnoreDynamicVisionImpairmentBuilder AddRequiredFeatures(IEnumerable<FeatureDefinition> features)
    {
        Definition.requiredFeatures.AddRange(features);
        return this;
    }

    public IgnoreDynamicVisionImpairmentBuilder AddForbiddenFeatures(params FeatureDefinition[] features)
    {
        Definition.forbiddenFeatures.AddRange(features);
        return this;
    }

    public IgnoreDynamicVisionImpairmentBuilder AddForbiddenFeatures(IEnumerable<FeatureDefinition> features)
    {
        Definition.forbiddenFeatures.AddRange(features);
        return this;
    }

    #region Constructors

    public IgnoreDynamicVisionImpairmentBuilder(string name, Guid namespaceGuid) : base(name, namespaceGuid)
    {
    }

    public IgnoreDynamicVisionImpairmentBuilder(string name, string definitionGuid) : base(name, definitionGuid)
    {
    }

    public IgnoreDynamicVisionImpairmentBuilder(FeatureDefinitionIgnoreDynamicVisionImpairment original,
        string name,
        Guid namespaceGuid) : base(original, name, namespaceGuid)
    {
    }

    public IgnoreDynamicVisionImpairmentBuilder(FeatureDefinitionIgnoreDynamicVisionImpairment original,
        string name,
        string definitionGuid) : base(original, name, definitionGuid)
    {
    }

    #endregion
}
