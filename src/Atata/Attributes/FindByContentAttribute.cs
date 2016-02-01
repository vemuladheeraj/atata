﻿namespace Atata
{
    public class FindByContentAttribute : TermFindAttribute
    {
        public FindByContentAttribute(TermMatch match)
            : base(match)
        {
        }

        public FindByContentAttribute(TermFormat format, TermMatch match = TermMatch.Inherit)
            : base(format, match)
        {
        }

        public FindByContentAttribute(string value, TermMatch match)
            : base(value, match)
        {
        }

        public FindByContentAttribute(params string[] values)
            : base(values)
        {
        }

        protected override TermFormat DefaultFormat
        {
            get { return TermFormat.Title; }
        }

        protected override TermMatch DefaultMatch
        {
            get { return TermMatch.Equals; }
        }

        public override IElementFindStrategy CreateStrategy(UIComponentMetadata metadata)
        {
            return new FindByContentStrategy();
        }

        protected override TermFormat GetTermFormatFromMetadata(UIComponentMetadata metadata)
        {
            var settingsAttribute = metadata.GetFirstOrDefaultGlobalAttribute<FindByContentSettingsAttribute>(x => x.Format != TermFormat.Inherit);
            return settingsAttribute != null ? settingsAttribute.Format : DefaultFormat;
        }

        protected override TermMatch GetTermMatchFromMetadata(UIComponentMetadata metadata)
        {
            var settingsAttribute = metadata.GetFirstOrDefaultGlobalAttribute<FindByContentSettingsAttribute>(x => x.Match != TermMatch.Inherit);
            return settingsAttribute != null ? settingsAttribute.Match : DefaultMatch;
        }
    }
}
