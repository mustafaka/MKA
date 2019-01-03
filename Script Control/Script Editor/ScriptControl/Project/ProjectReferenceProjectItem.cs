using System;
using System.ComponentModel;

namespace AIMS.Libraries.Scripting.ScriptControl.Project
{
    public class ProjectReferenceProjectItem : ReferenceProjectItem
    {
        IProject referencedProject;

        [Browsable(false)]
        public IProject ReferencedProject
        {
            get
            {
                if (referencedProject == null)
                    referencedProject = ScriptControl.GetProject();
                return referencedProject;
            }
        }

        [ReadOnly(true)]
        public string ProjectGuid
        {
            get
            {
                return GetEvaluatedMetadata("Project");
            }
            set
            {
                SetEvaluatedMetadata("Project", value);
            }
        }

        [ReadOnly(true)]
        public string ProjectName
        {
            get
            {
                return GetEvaluatedMetadata("Name");
            }
            set
            {
                SetEvaluatedMetadata("Name", value);
            }
        }

        
        public ProjectReferenceProjectItem(IProject project, IProject referenceTo)
            : base(project, ItemType.ProjectReference)
        {
            this.Include = FileUtility.GetRelativePath(project.Directory, referenceTo.FileName);
            //ProjectGuid = referenceTo.IdGuid;
            //ProjectName = referenceTo.Name;
            this.referencedProject = referenceTo;
        }
    }
}
