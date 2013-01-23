/*
   Copyright 2012 Michael Edwards
 
   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 
*/ 
//-CRE-

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Glass.Mapper.Pipelines.ConfigurationResolver;
using Glass.Mapper.Pipelines.ConfigurationResolver.Tasks.StandardResolver;
using Glass.Mapper.Pipelines.DataMapperResolver;
using Glass.Mapper.Pipelines.DataMapperResolver.Tasks;
using Glass.Mapper.Pipelines.ObjectConstruction;
using Glass.Mapper.Pipelines.ObjectConstruction.Tasks.CreateConcrete;
using Glass.Mapper.Pipelines.ObjectConstruction.Tasks.CreateInterface;
using Glass.Mapper.Pipelines.ObjectSaving;
using Glass.Mapper.Pipelines.ObjectSaving.Tasks;
using Glass.Mapper.Pipelines.TypeResolver;
using Glass.Mapper.Pipelines.TypeResolver.Tasks.StandardResolver;
using Glass.Mapper.Sc.DataMappers;

namespace Glass.Mapper.Sc.Website
{
    public class GlassConfig : GlassCastleConfigBase
    {
        public override void Configure(WindsorContainer container, string contextName)
        {
            container.Register(

                Component.For<AbstractDataMapper>().ImplementedBy<SitecoreInfoMapper>().LifestyleTransient(),
                Component.For<AbstractDataMapper>().ImplementedBy<SitecoreIdMapper>().LifestyleTransient(),
                Component.For<AbstractDataMapper>().ImplementedBy<SitecoreChildrenMapper>().LifestyleTransient(),
                Component.For<AbstractDataMapper>().ImplementedBy<SitecoreParentMapper>().LifestyleTransient(),


            //****** Data Mapper Resolver Tasks ******//
                // These tasks are run when Glass.Mapper tries to resolve which DataMapper should handle a given property, e.g. 
                // Tasks are called in the order they are specified below.
                // For more on component registration read: http://docs.castleproject.org/Windsor.Registering-components-one-by-one.ashx
                Component.For<IDataMapperResolverTask>().ImplementedBy<DataMapperStandardResolverTask>().LifestyleTransient(),

            //****** Type Resolver Tasks ******//
                // These tasks are run when Glass.Mapper tries to resolve the type a user has requested, e.g. 
                // if your code contained
                //       service.GetItem<MyClass>(id) 
                // the standard resolver will return MyClass as the type. You may want to specify your own tasks to custom type
                // inferring.
                // Tasks are called in the order they are specified below.
                // For more on component registration read: http://docs.castleproject.org/Windsor.Registering-components-one-by-one.ashx

                Component.For<ITypeResolverTask>().ImplementedBy<TypeStandardResolverTask>().LifestyleTransient(),

            //****** Configuration Resolver Tasks ******//
                // These tasks are run when Glass.Mapper tries to find the configration the user has requested based on the type passsed, e.g. 
                // if your code contained
                //       service.GetItem<MyClass>(id) 
                // the standard resolver will return the MyClass configuration. 
                // Tasks are called in the order they are specified below.
                // For more on component registration read: http://docs.castleproject.org/Windsor.Registering-components-one-by-one.ashx

                Component.For<IConfigurationResolverTask>().ImplementedBy<ConfigurationStandardResolverTask>().LifestyleTransient(),

            //****** Object Construction Tasks ******//
                // These tasks are run when an a class needs to be instantiated by Glass.Mapper.
                // Tasks are called in the order they are specified below.
                // For more on component registration read: http://docs.castleproject.org/Windsor.Registering-components-one-by-one.ashx

                Component.For<IObjectConstructionTask>().ImplementedBy<CreateConcreteTask>().LifestyleTransient(),
                Component.For<IObjectConstructionTask>().ImplementedBy<CreateInterfaceTask>().LifestyleTransient(),

                        //****** Object Saving Tasks ******//
                // These tasks are run when an a class needs to be saved by Glass.Mapper.
                // Tasks are called in the order they are specified below.
                // For more on component registration read: http://docs.castleproject.org/Windsor.Registering-components-one-by-one.ashx

                Component.For<IObjectSavingTask>().ImplementedBy<StandardSavingTask>().LifestyleTransient()
            );
        }
    }
}

