using System.Text.Json;
using EPiServer.DataAccess;
using EPiServer.Security;
using EPiServer.ServiceLocation;
using EPiServer.VisualBuilder;
using OptiAlloy.Models.Elements;

namespace OptiAlloy.Business;

[ServiceConfiguration(Lifecycle = ServiceInstanceScope.Singleton)]
public sealed class TestDataFactory(
    IContentRepository contentRepository,
    IContentTypeRepository contentTypeRepository,
    IBlockPropertyFactory blockPropertyFactory,
    IDisplayTemplateRepository displayTemplateRepository)
{
    // To change the test data, move the contentKey Guid into obsoleteTestContent and replace
    // contentKey with a new Guid. This will remove the old test page and create a new one instead.
    private readonly Guid[] obsoleteTestContent = [];
    private readonly Guid contentKey = new("0A806C91-504A-4308-AEE9-9B7D814E87BE");

    public void Ensure()
    {
        // First, remove obsolete test data
        foreach (var key in obsoleteTestContent)
        {
            if (contentRepository.TryGet<IContent>(key, out var obsolete))
            {
                contentRepository.Delete(obsolete.ContentLink, true, AccessLevel.NoAccess);
            }
        }

        // Create test content if it doesn't exist
        if (!contentRepository.TryGet<BlankExperience>(contentKey, out var experience))
        {
            var sectionId = Guid.NewGuid();
            var headingId = Guid.NewGuid();
            var richtextId = Guid.NewGuid();

            var heading2Id = Guid.NewGuid();
            var richtext2Id = Guid.NewGuid();

            // Create the new experience with layout
            experience = contentRepository.GetDefault<BlankExperience>(ContentReference.StartPage);
            experience.ContentGuid = contentKey;
            experience.Name = "Test Experience";
            var experienceLayout = new Layout
            {
                Type = LayoutType.Outline,
                Nodes = new List<LayoutNode>
                {
                    new LayoutSectionNode
                    {
                        //Id = Guid.NewGuid().ToString("N"),
                        Name = "Blank Section",
                        PropertyBinding = $"UnstructuredData[{sectionId}]"
                    }
                }
            };
            experience.Layout = experienceLayout;
            var experienceData = experience.UnstructuredData ?? new ContentArea();

            // Create a blank section with layout
            var section = blockPropertyFactory.Create<BlankSection>();
            var sectionLayout = new Layout
            {
                Type = LayoutType.Grid,
                Nodes = new List<LayoutNode>
                {
                    new LayoutStructureNode(LayoutStructureType.Row)
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Row",
                        Nodes = new List<LayoutNode>
                        {
                            new LayoutStructureNode(LayoutStructureType.Column)
                            {
                                Id = Guid.NewGuid().ToString(),
                                Name = "Column",
                                Nodes = new List<LayoutNode>
                                {
                                    new LayoutComponentNode
                                    {
                                        //Id = Guid.NewGuid().ToString("N"),
                                        Name = "Heading",
                                        PropertyBinding = $"UnstructuredData[{headingId}]"
                                    },
                                    new LayoutComponentNode
                                    {
                                        //Id = Guid.NewGuid().ToString("N"),
                                        Name = "Paragraph",
                                        PropertyBinding = $"UnstructuredData[{richtextId}]"
                                    }
                                }
                            },
                            new LayoutStructureNode(LayoutStructureType.Column)
                            {
                                Id = Guid.NewGuid().ToString(),
                                Name = "Column",
                                Nodes = new List<LayoutNode>
                                {
                                    new LayoutComponentNode
                                    {
                                        //Id = Guid.NewGuid().ToString("N"),
                                        Name = "Heading",
                                        PropertyBinding = $"UnstructuredData[{heading2Id}]"
                                    },
                                    new LayoutComponentNode
                                    {
                                        //Id = Guid.NewGuid().ToString("N"),
                                        Name = "Paragraph",
                                        PropertyBinding = $"UnstructuredData[{richtext2Id}]"
                                    }
                                }
                            }
                        }
                    }
                }
            };

            section.Layout = sectionLayout;
            var sectionData = section.UnstructuredData ?? new ContentArea();

            // Create and add heading element
            var headingElement = blockPropertyFactory.Create<HeadingElement>();
            headingElement.Body = "Lorem ipsum";
            sectionData.Items.Add(new ContentAreaItem
            {
                RenderSettings =
                {
                    { "data-epi-block-id", headingId.ToString() }
                },
                InlineBlock = headingElement
            });

            // Create and add richtext element
            var richtextElement = blockPropertyFactory.Create<RichTextElement>();
            richtextElement.Body = new XhtmlString("<p>Morbi leo risus, porta ac consectetur ac, vestibulum at eros. Etiam porta sem malesuada magna mollis euismod. Integer commodo bibendum sapien, vitae dapibus odio aliquet id. Nullam quis risus eget urna mollis ornare vel eu leo. Morbi leo risus, porta ac consectetur ac, vestibulum at eros. Integer posuere erat a ante venenatis dapibus posuere velit aliquet. Praesent commodo cursus magna, vel scelerisque nisl consectetur et. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Etiam porta sem malesuada magna mollis euismod.</p>");
            sectionData.Items.Add(new ContentAreaItem
            {
                RenderSettings =
                {
                    { "data-epi-block-id", richtextId.ToString() }
                },
                InlineBlock = richtextElement
            });

            // Create and add heading element
            var heading2Element = blockPropertyFactory.Create<HeadingElement>();
            heading2Element.Body = "Vel magnased";
            sectionData.Items.Add(new ContentAreaItem
            {
                RenderSettings =
                {
                    { "data-epi-block-id", heading2Id.ToString() }
                },
                InlineBlock = heading2Element
            });

            // Create and add richtext element
            var richtext2Element = blockPropertyFactory.Create<RichTextElement>();
            richtext2Element.Body = new XhtmlString("<p>Nullam quis risus eget urna mollis ornare vel eu leo. Integer commodo bibendum sapien, vitae dapibus odio aliquet id. Cras mattis consectetur purus sit amet fermentum. Mauris sollicitudin, urna eu molestie pharetra, ipsum est pulvinar purus. Aliquam erat volutpat in congue etiam justo etiam. Vivamus sagittis lacus vel augue laoreet rutrum faucibus dolor auctor. Maecenas faucibus mollis interdum. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Mauris sollicitudin, urna eu molestie pharetra, ipsum est pulvinar purus.</p>");
            sectionData.Items.Add(new ContentAreaItem
            {
                RenderSettings =
                {
                    { "data-epi-block-id", richtext2Id.ToString() }
                },
                InlineBlock = richtext2Element
            });

            section.UnstructuredData = sectionData;

            // Add section to experience
            experienceData.Items.Add(new ContentAreaItem
            {
                RenderSettings =
                {
                    { "data-epi-block-id", sectionId.ToString() }
                },
                InlineBlock = section
            });
            experience.UnstructuredData = experienceData;

            contentRepository.Save(experience, SaveAction.Publish, AccessLevel.NoAccess);
        }

        var options = new JsonSerializerOptions();
        options.Converters.Add(new ContentTypeBaseConverter());

        // Now let's create all display templates if we don't have any
        if (!displayTemplateRepository.List().Any())
        {
            // Load JSON data
            using var file = File.OpenRead("./displaytemplates.json");
            using var stream = new StreamReader(file);

            var json = stream.ReadToEnd();
            var templates = JsonSerializer.Deserialize<List<DisplayTemplate>>(json, options);

            // use proper ContentTypeID for EditorialBlock
            var editorialBlocks = templates.Where(x => x.ContentTypeID == 5555).ToList();
            var editorialBlockType = contentTypeRepository.List().FirstOrDefault(x => x.Name == "EditorialBlock");
            for (var index = 0; index < editorialBlocks.Count; index++)
            {
                var displayTemplate = editorialBlocks[index];
                displayTemplate.ContentTypeID = editorialBlockType.ID;
            }

            foreach (var template in templates)
            {
                displayTemplateRepository.Save(template);
            }
        }
    }
}
