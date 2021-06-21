# **基于.NETCore的轻量级开发框架--简介**

## **目录**

1. <a href="#1">架构设计思路解读</a>
2. <a href="#2">框架层级介绍</a>
3. <a href="#3">领域模型解读及实现</a>

    1. <a href="#31">Utilities</a>
    2. <a href="#32">Entities</a>
    3. <a href="#33">ViewModels</a>
    4. <a href="#34">Service</a>
    5. <a href="#35">API</a>

4. <a href="#4">使用规范</a>
5. <a href="#5">框架规划</a>

---

## **<a name="1" id="1">1.架构设计思路解读</a>**

我们按照领域驱动设计的核心思想，采用了目前行业比较常用的六边形架构为基础来进行框架的设计与搭建。当然了，我们的目标并不是搭建一个重量级的企业框架，从我们实际面对的业务出发，我们需要的应该是一个轻量级的框架。而且，作为内部使用的框架来说，我们要做到最大限度的源码可读性、扩展性与架构的开放性。
我们并不认为目前的结构很好很完整，对于架构的持续升级将会作为我们工作的一部分，当然我们对于框架底层的一些升级会在慎重考虑后进行。
首先，我们希望大家理解什么是六边形架构。

> 六边形架构(Hexagonal Architecture), 又叫做端口适配器模式(Ports & Adapters)。
> 六边形架构是基于三个原则和技术来开展实施：
> 1. 明确区分应用程序，领域和基础结构三个层
> 2. 依赖关系是从应用程序和基础结构再到领域
> 3. 使用端口和适配器隔离它们的边界

当然，我们目前的体量如果严格按照上述三个原则来进行框架设计，那么我们的开发人员必然会面对一些头疼的问题，如业务抽象程度过低，开发前过于依赖上层人员对业务逻辑的抽象整合，以及势必需要编写更多的代码来实现看起来并不是那么复杂的业务逻辑。所以在理解三个原则后，我们会将内部的架构层次进行一些整合与调整以适配我们目前的实际情况。

---

## **<a name="2" id="2">2.框架层级介绍</a>**

### **2.1 应用程序层**
应用程序层是用户或外部程序与应用程序进行交互的一面。对于我们来说最常见的情况就是基于Rest标准的API的HTTP路由层。最常规的实现就是API项目中的`Controller`代码，我们使用基于路由匹配的方式来进行HTTP路由的匹配。
当然应用程序层并不只是HTTP路由，如果业务有需要，我们也可以基于其他协议来开放更多的端口和与之对应的适配器。
应用程序层也有另外一个名称，这在六边形架构的设计中时而看见，叫做北向网关，在六边形架构中，我们通常规定数据的流向为从南到北（或者说从上到下），所以北向网关可以简单理解为应用程序的访问入口端口及适配器所在的层级。

### **2.2领域**
左右两端隔离的部分，它包含所有涉及并实现业务逻辑的代码。领域层往往是应用程序源代码中占比最大的部分。领域中首选需要暴露的是针对北向网关的适配器实现。 
领域层中还需要针对基础架构来实现南向网关的使用。这是业务逻辑从具体到抽象层面的一个转换。这个转换的过程完全在领域层中实现，而且无需框架的其它层面来关注这个实现。
严格来说领域层也可以细分为多个层级来更好的区分工程代码的性质，不过我们认为目前没有这个必要来做如此细节的区分，当然我们使用不同的文件和目录来对这些工程代码加以区分和隔离存放，但是在具体的业务工作中，我们允许在某些规范下进行开发实践以帮助开发人员可以更专注在开发工作中而非代码性质的隔离性上。

*当然严格来说这些适配器的规定应该是一个抽象的存在，不过我们在现有条件的实践中发现，我们不得不在领域层中提供很多与业务逻辑紧密联系且复用程度很低的适配器实现，所以我们并不会将所有抽象都在框架层面进行锁定，以方便开发人员在实际工作中可以更直观的来拓展自己的北向适配器。*

### **2.3 基础架构**
在这里我们可以找到应用程序需要什么，它可以驱动什么工作。它包含的基础结构详细信息。例如与数据库交互的驱动、引擎，对文件系统的调用或处理一些抽象程度极高且非常细节的代码。当然，随着业务的发展，基础架构层需要提供更多的服务能力。

---

## **<a name="3" id="3">3.领域模型解读及实现</a>**



我们将会针对框架的一个简单实现来描述每个层级或者说每个用户代码文件所负责的内容及其工作的流程。我们尽量在这个过程中使用抽象的方式来描述各个模型。如果你想找到关于复杂业务的框架实践，你可以通过其他方式来找到对应的实践，例如去现有的应用程序中寻找类似的实践或求助于你的同事来指导你进行实践。

### **<a name="31" id="31">3.1 Utilities</a>**



`Utilities`里包含的是所有我们认为高度抽象的模型以及公共服务能力的实现。
你可以在 `Gx.cs` 中找到目前我们提供的常用服务能力;你可以在`Enum.cs`中找打目前所有的枚举定义，包含公共的枚举定义以及基于业务的枚举定义，不要担心，这些代码并不需要开发人员来逐一编写。
你可以在`Models`文件夹中找到所有高度抽象且复用性极高的模型，熟读这些模型，你会在很多地方需要用到他们的。

```c#
    /// <summary>
    /// 统一返回结构体，适用于HTTP请求响应的统一结构
    /// </summary>
    /// <typeparam name="T">根据实际返回的ViewModel进行传输</typeparam>
    public class ResultModel<T> where T : class
    {
        /// <summary>
        /// 响应结果状态码
        /// 0-成功；500-内部错误；10X-数据操作失败；X-业务规则验证失败
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 响应结果信息描述
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 响应结果数据
        /// </summary>
        public T Data { get; set; }
    }
```
```c#
    /// <summary>
    /// 分页查询结果结构体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageModel<T> where T : class
    {
        /// <summary>
        /// 数据总量
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 当前页的数据集合
        /// </summary>
        public IEnumerable<T> Items { get; set; }
    }
```
```c#
    /// <summary>
    /// 排序类用数据模型
    /// </summary>
    public class OrderByModel
    {
        public OrderByModel()
        { }

        public OrderByModel(string orderByFieldName, SortType type)
        {
            this.OrderByFieldName = orderByFieldName;
            this.Type = type;
        }


        /// <summary>
        /// 字段名称
        /// </summary>
        public string OrderByFieldName { get; set; }

        /// <summary>
        /// 排列顺序
        /// </summary>
        public SortType Type { get; set; }
    }
```
```c#
    /// <summary>
    /// 文件信息记录类，上传标准结构
    /// </summary>
    public class FileModel
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int Sn { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string Url { get; set; }
    }
```
```c#
    /// <summary>
    /// 用于保存枚举数据对象
    /// </summary>
    public class EnumModel
    {
        public EnumModel() { }

        public EnumModel(string key, int val)
        {
            Key = key;
            Value = val;
        }

        public string Key { get; set; }

        public int Value { get; set; }
    }
```
```c#
    /// <summary>
    /// 自定义SQL参数用到的数据模型
    /// </summary>
    public class SqlParameterModel
    {
        public SqlParameterModel()
        { }

        public SqlParameterModel(string name, DbType type, object value)
        {
            this.Name = name;
            this.Type = type;
            this.Value = value;
        }

        /// <summary>
        /// 参数名称，用@做为标识前缀
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 参数的数据类型
        /// </summary>
        public DbType Type { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public object Value { get; set; }
    }
```
在大部分情况下，你应该是不需要在`Utilities`中创建任何文件或者编写任何代码的，如果你认为确实有必要修改这部分内容，请务必和同事讨论完毕且达成统一意见之后再进行修改。并且你应该为`Utilities`中的代码修改单独进行一次版本提交并加以说明。

### **<a name="32" id="32">3.2 Entites</a>**

如果你有过任何ORM使用的经验，那么你可以很轻松的理解这部分内容。因为传统的ORM组件，在模型定义的部分区别都不是很大。当然，如果你没有ORM的实际经验的话，也不用担心，我们可以使用非常简单的语言来描述这部分内容的意义。
> 按照你的数据结构，对数据库的每一个表转化为一个数据模型，包括字段名称，类型以及字段的说明。每一个字段都会对应一个类中的一个属性。
> 你的数据模型范围可以超过你的数据结构，但是注意标记清楚超出范围的那些东西在何时使用。
    
ORM就是这样，我们有许多办法来将数据结构转化为数据模型，在框架中我们使用的是`SqlSugarCore`这样的一个ORM组件，所以我们需要遵循它的规范。而且我们还需要对数据模型保证足够的掌控力以满足日益复杂的业务需求整合。我们可以通过一个典型的数据模型来看一下99%情况下都可以满足的开发实践。

```c#
    /// <summary>
    /// 针对数据库中的表一对一转化的模型
    /// 需要通过特性指定表名，通过特性指定主键属性，主键类型
    /// </summary>
    [SugarTable("dbo.Material")]
    public class Material : BaseEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int MaterialID { get; set; }
        public string MaterialName { get; set; }
        public int MaterialTypeID { get; set; }
        public int MaterialFeatureID { get; set; }
        public int MaterialCostID { get; set; }
        public int MaterialNatureID { get; set; }
        public bool IsAlwaysAvailable { get; set; }
        public bool IsAttachTool { get; set; }
        public bool IsConsumable { get; set; }
        public bool IsUniformPurchase { get; set; }
        public string Code { get; set; }
        public int ImportantLevel { get; set; }
        public string Remark { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
```
```c#
    /// <summary>
    /// 针对数据模型的拓展，每个属性都需要添加特性来标明这个属性并不是与数据库字段的对应
    /// 尽管不加属性并不会引发什么问题，但是我们还是希望在添加该特性来增加代码的完整性
    /// </summary>
    public class MaterialExtension : Material
    {

        [SugarColumn(IsIgnore = true)]
        public string Specification { get; set; }
        [SugarColumn(IsIgnore = true)]
        public string Texture { get; set; }
        [SugarColumn(IsIgnore = true)]
        public string Designation { get; set; }
        [SugarColumn(IsIgnore = true)]
        public string MeasurementUnit { get; set; }
        [SugarColumn(IsIgnore = true)]
        public string BaseValue { get; set; }
        [SugarColumn(IsIgnore = true)]
        public string ConversionUnit { get; set; }
        [SugarColumn(IsIgnore = true)]
        public int ConversionExpression { get; set; }
        [SugarColumn(IsIgnore = true)]
        public string Draw_Code { get; set; }
        [SugarColumn(IsIgnore = true)]
        public string Attr_Files { get; set; }
        [SugarColumn(IsIgnore = true)]
        public string Draw_Imgs { get; set; }
        [SugarColumn(IsIgnore = true)]
        public string CostName { get; set; }
        [SugarColumn(IsIgnore = true)]
        public string FeatureName { get; set; }
        [SugarColumn(IsIgnore = true)]
        public string NatureName { get; set; }
        [SugarColumn(IsIgnore = true)]
        public string MaterialTypeName { get; set; }
    }
```

也许你会觉得代码很冗长且枯燥，没关系。继承于`BaseEntity`的类并不需要开发人员来手动编写，而且任何情况下你都不应该去修改这部分代码。如果业务过程中需要,你可以在`XXXExtension.cs`中来进行代码的编写。
你需要注意的是，`Entities`中只是用来存放数据模型的，永远不要在这里处理任何的业务逻辑，哪怕它非常的简单和直观，你也不应该在这里编写任务带有逻辑性的代码。同时，不要尝试通过get或set方法来对某个属性进行操作，这将会给你在后续工作中造成极大的麻烦。

### **<a name="33" id="33">3.3 ViewModels</a>**

`ViewModels`，有的时候我们也会将其命名为`Models`。
我们坚持认为，数据模型不能直接暴露为应用程序的使用者，尽管在我们的业务中，大部分时间数据模型的内容可以直接使用，但是维持一次开销极小的转化是很有必要的，并且这为业务数据的隔离提供了可能性。
当然框架会生成一部分`ViewModel`的代码，你需要在这里做的是根据实际的业务需求来扩展这些模型以满足业务要求。
我们了解，为每个不同的业务服务创建独立的数据模型是一个耦合度很低的策略，但是我们不认为我们的体量可以有足够的时间来支撑我们做这些事情，所以大部分时间我们总是针对同一业务部分的服务使用相同的模型，这回带来一个问题，那就是某些模型中的某些属性并不总是有效。从眼下的情况来看，你需要通过与对应的同事加强沟通的方式来解决这个问题。
`ViewModels`需要对每一个模型去重写(`override`)几个验证方法来验证输入模型的正确性。我们也将尝试通过生成代码的方式来帮助开发人员降低这些简单的数据验证工作。
每一个模型都需要有视图模型与查询模型。

```c#
    public class MaterialTypeModel : BaseViewModel
    {
        public int MaterialTypeID { get; set; }
        public string MaterialTypeName { get; set; }
        public string CodeRule { get; set; }
        public int ParentID { get; set; }
        public string Remark { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public override bool IsCheckModelDataValidate()
        {
            if (MaterialTypeID <= 0)
                return false;

            return IsCheckModelDataValidateWithoutID();
        }

        public override bool IsCheckModelDataValidateWithoutID()
        {
            if (string.IsNullOrEmpty(MaterialTypeName))
                return false;
            if (ParentID <= 0)
                return false;

            return true;
        }
    }
```
```c#
    public class MaterialTypeFilterModel : BaseFilterModel
    {
        public string _MaterialTypeName { get; set; }

        public bool? _IsDelete { get; set; }
    }
```
请注意上述代码示例中的继承关系，千万不要忘记了。我们在`FilterModel`中已经预定了一个属性用来在关键字的查询(`_SearchInput`)，你将在后续的代码中看到它如何工作。

### **<a name="34" id="34">3.4 Service</a>**

毫无疑问，这将是代码占比最大的一部分，因为他需要通过黑盒的方式提供所有需要的业务逻辑实现。Service中主要分为三个部分，我们为他们提供了基础的命名分隔规范：`Logic`、`Mapping`、`Repository`。在这之中，我们三中不同的文件进行了一个大概的规约设定：
* `Logic` 你可以在这里编写所有基于数据集合或数据模型实例的业务代码，包括对于数据的转化、计算、封装。这里暴露出来的接口将作为北向网关的适配器实现，所以别忘了满足北向网关的要求。但是请注意，这里的重点应该放在业务的逻辑规则部分，不要在这里尝试从数据库中抽取数据。
* `Repository` 你可以在这里实现所有你需要的数据服务，包括数据的查询与写入。当然框架已经提供了基础的读写能力。不过如果你确实需要进行复查的查询或者长事务的控制，那么你需要在这里扩展自己的接口并实现它。因为我们还希望这个框架可以在但数据库的情况下进行Saas的支持，所以你需要通过框架提供的查询引擎或者写入引擎来实际执行数据库的操作，这些引擎会自动的帮你进行多租户的数据隔离。如果你需要编写复杂的查询但找不到相关的实践参考，你也可以去[SqlSugar](https://www.donet5.com/Doc/1/1187)的官方文档上去寻找一些示例。
* `mapping` 在这里你可以做`Entities`和`ViewModels`的转化以及查询模型的表达式转化。你很容易理解这部分代码，因为他们仅仅是在做对应，永远不要尝试在这里进行任何查询操作。如果你的视图模型(`ViewModel`)需要扩展其他属性，那么你可以选择重载转换方法或者在`Logic`里进行某些特殊属性的转化，我们允许你根据情况来灵活选择实现的策略。

我们在基础代码中规定了一些基础服务能力的接口并给出了默认实现，对于某些简单的业务，你应该是不需要处理什么东西了。当然这也可以作为你在`Service`中的编码参考存在。
下面这些代码你完全可以跳过，在工作中来逐步的阅读它。
如果你想对服务层的编码方式和工作方式做一些记录了解，那么可以尝试去理解以下的代码，他代表着一个基础的服务全功能的领域实现，这看起来似乎会很长，但是请注意理解里面的思想与规范而不是具体的实现细节。因为很多统一化的细节可以由代码生成器来帮助开发者完成对应工作。

```c#
    public interface IMaterialCostLogic : ILogic<MaterialCostModel, MaterialCostFilterModel>
    {

    }

    public class MaterialCostLogic : IMaterialCostLogic
    {
        private readonly IMaterialCostMapping _mapping;
        private readonly IMaterialCostRepository _repository;

        public MaterialCostLogic(IMaterialCostMapping __IMaterialCostMapping, IMaterialCostRepository __IMaterialCostRepository)
        {
            this._mapping = __IMaterialCostMapping;
            this._repository = __IMaterialCostRepository;
        }

        public ResultModel<ValueViewModel> Delete(int id)
        {
            ResultModel<ValueViewModel> result = new ResultModel<ValueViewModel>();
            try
            {
                ValueViewModel model = new ValueViewModel();
                int flag = _repository.Delete(id);
                if (flag > 0)
                {
                    model.ServiceIdentity = true;
                    model.Data = flag;
                }
                else
                {
                    model.ServiceIdentity = false;
                }

                result.Code = 0;
                result.Data = model;
                result.Msg = "success";
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Msg = ex.Message.ToString();
            }
            return result;
        }

        public ResultModel<ValueViewModel> Delete(List<int> ids)
        {
            ResultModel<ValueViewModel> result = new ResultModel<ValueViewModel>();
            try
            {
                ValueViewModel model = new ValueViewModel();
                _repository.BatchDelete(ids);

                model.ServiceIdentity = true;
                model.Data = 0;

                result.Code = 0;
                result.Data = model;
                result.Msg = "success";
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Msg = ex.Message.ToString();
            }
            return result;
        }

        public ResultModel<IEnumerable<MaterialCostModel>> GetAll(string orderBy = null)
        {
            ResultModel<IEnumerable<MaterialCostModel>> result = new ResultModel<IEnumerable<MaterialCostModel>>();
            try
            {
                var list = _repository.GetAll().ToList().Select(x => _mapping.Entity2Model(x));

                result.Code = 0;
                result.Msg = "success";
                result.Data = list;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Msg = ex.Message.ToString();
            }

            return result;
        }

        public ResultModel<MaterialCostModel> GetByID(int id)
        {
            ResultModel<MaterialCostModel> result = new ResultModel<MaterialCostModel>();
            try
            {
                var model = _mapping.Entity2Model(_repository.GetModelByPrimaryKey(id));

                result.Code = 0;
                result.Msg = "success";
                result.Data = model;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Msg = ex.Message.ToString();
            }

            return result;
        }

        public ResultModel<ValueViewModel> GetCount(MaterialCostFilterModel filterModel)
        {
            ResultModel<ValueViewModel> result = new ResultModel<ValueViewModel>();
            try
            {
                ValueViewModel model = new ValueViewModel();
                int iCount = _repository.GetCount(_mapping.FilterModel2Where(filterModel));

                model.ServiceIdentity = true;
                model.Data = iCount;

                result.Code = 0;
                result.Data = model;
                result.Msg = "success";
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Msg = ex.Message.ToString();
            }
            return result;
        }

        public ResultModel<MaterialCostModel> GetModel(MaterialCostFilterModel filterModel, string orderBy = null)
        {
            ResultModel<MaterialCostModel> result = new ResultModel<MaterialCostModel>();
            try
            {
                var model = _mapping.Entity2Model(_repository.GetModel(_mapping.FilterModel2Where(filterModel), orderBy));

                result.Code = 0;
                result.Msg = "success";
                result.Data = model;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Msg = ex.Message.ToString();
            }

            return result;
        }

        public ResultModel<PageModel<MaterialCostModel>> GetPage(MaterialCostFilterModel filterModel, int pageIndex, int pageSize, string orderBy = null)
        {
            ResultModel<PageModel<MaterialCostModel>> result = new ResultModel<PageModel<MaterialCostModel>>();
            try
            {
                PageModel<MaterialCostModel> pageResult = new PageModel<MaterialCostModel>();


                var expr = _mapping.FilterModel2Where(filterModel);
                pageResult.TotalCount = _repository.GetCount(expr);

                var list = _repository.GetPage(_mapping.FilterModel2Where(filterModel), pageIndex, pageSize, orderBy).ToList().Select(x => _mapping.Entity2Model(x));
                pageResult.Items = list;

                result.Code = 0;
                result.Msg = "success";
                result.Data = pageResult;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Msg = ex.Message.ToString();
            }

            return result;
        }

        public ResultModel<IEnumerable<MaterialCostModel>> GetSearch(MaterialCostFilterModel filterModel, string orderBy = null, int? top = null)
        {
            ResultModel<IEnumerable<MaterialCostModel>> result = new ResultModel<IEnumerable<MaterialCostModel>>();
            try
            {
                var list = _repository.GetSearch(_mapping.FilterModel2Where(filterModel), orderBy).ToList().Select(x => _mapping.Entity2Model(x));

                result.Code = 0;
                result.Msg = "success";
                result.Data = list;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Msg = ex.Message.ToString();
            }

            return result;
        }

        public ResultModel<MaterialCostModel> Insert(MaterialCostModel model)
        {
            ResultModel<MaterialCostModel> result = new ResultModel<MaterialCostModel>();
            try
            {
                var flag = _repository.Insert(_mapping.Model2Entity(model));
                model.CostID = flag;

                result.Code = 0;
                result.Msg = "success";
                result.Data = model;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Msg = ex.Message.ToString();
            }

            return result;
        }

        public ResultModel<MaterialCostModel> Insert(List<MaterialCostModel> models)
        {
            throw new NotImplementedException();
        }

        public ResultModel<MaterialCostModel> Update(MaterialCostModel model)
        {
            ResultModel<MaterialCostModel> result = new ResultModel<MaterialCostModel>();
            try
            {
                var flag = _repository.Update(_mapping.Model2Entity(model));

                result.Code = 0;
                result.Msg = "success";
                result.Data = model;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Msg = ex.Message.ToString();
            }

            return result;
        }

        public ResultModel<MaterialCostModel> Update(List<MaterialCostModel> models)
        {
            throw new NotImplementedException();
        }

        public bool _Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void _Delete(List<int> ids)
        {
            throw new NotImplementedException();
        }

        public List<MaterialCostModel> _GetAll(string orderBy = null)
        {
            throw new NotImplementedException();
        }

        public MaterialCostModel _GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public int _GetCount(MaterialCostFilterModel filterModel)
        {
            throw new NotImplementedException();
        }

        public MaterialCostModel _GetModel(MaterialCostFilterModel filterModel, string orderBy = null)
        {
            throw new NotImplementedException();
        }

        public PageModel<MaterialCostModel> _GetPage(MaterialCostFilterModel filterModel, int pageIndex, int pageSize, string orderBy = null)
        {
            throw new NotImplementedException();
        }

        public List<MaterialCostModel> _GetSearch(MaterialCostFilterModel filterModel, string orderBy = null, int? top = null)
        {
            throw new NotImplementedException();
        }

        public int _Insert(MaterialCostModel model)
        {
            throw new NotImplementedException();
        }

        public List<int> _Insert(List<MaterialCostModel> models)
        {
            throw new NotImplementedException();
        }

        public bool _Update(MaterialCostModel model)
        {
            throw new NotImplementedException();
        }

        public void _Update(List<MaterialCostModel> models)
        {
            throw new NotImplementedException();
        }
    }
```
```c#
    public interface IMaterialMapping : IMapping<MaterialExtension, MaterialModel, MaterialFilterModel>
    {

    }

    public class MaterialMapping : BaseMapping<MaterialExtension, MaterialModel, MaterialFilterModel>, IMaterialMapping
    {
        public MaterialModel Entity2Model(MaterialExtension entity)
        {
            MaterialModel model = new MaterialModel();

            model.MaterialID = entity.MaterialID;
            model.MaterialName = entity.MaterialName;
            model.MaterialTypeID = entity.MaterialTypeID;
            model.MaterialFeatureID = entity.MaterialFeatureID;
            model.MaterialCostID = entity.MaterialCostID;
            model.MaterialNatureID = entity.MaterialNatureID;
            model.IsAlwaysAvailable = entity.IsAlwaysAvailable;
            model.IsAttachTool = entity.IsAttachTool;
            model.IsConsumable = entity.IsConsumable;
            model.IsUniformPurchase = entity.IsUniformPurchase;
            model.Code = entity.Code;
            model.ImportantLevel = entity.ImportantLevel;
            model.Remark = entity.Remark;
            model.IsDelete = entity.IsDelete;
            model.CreateDate = entity.CreateDate;
            model.UpdateDate = entity.UpdateDate;

            return model;
        }

        public IEnumerable<MaterialModel> Entity2Model(IEnumerable<MaterialExtension> entities)
        {
            List<MaterialModel> list = new List<MaterialModel>();
            foreach (var item in entities)
            {
                list.Add(Entity2Model(item));
            }
            return list;
        }

        public MaterialExtension Model2Entity(MaterialModel model)
        {
            MaterialExtension entity = new MaterialExtension();

            entity.MaterialID = model.MaterialID;
            entity.MaterialName = model.MaterialName;
            entity.MaterialTypeID = model.MaterialTypeID;
            entity.MaterialFeatureID = model.MaterialFeatureID;
            entity.MaterialCostID = model.MaterialCostID;
            entity.MaterialNatureID = model.MaterialNatureID;
            entity.IsAlwaysAvailable = model.IsAlwaysAvailable;
            entity.IsAttachTool = model.IsAttachTool;
            entity.IsConsumable = model.IsConsumable;
            entity.IsUniformPurchase = model.IsUniformPurchase;
            entity.Code = model.Code;
            entity.ImportantLevel = model.ImportantLevel;
            entity.Remark = model.Remark;
            entity.IsDelete = model.IsDelete;
            entity.CreateDate = model.CreateDate;
            entity.UpdateDate = model.UpdateDate;

            return entity;
        }

        public IEnumerable<MaterialExtension> Model2Entity(IEnumerable<MaterialModel> models)
        {
            List<MaterialExtension> list = new List<MaterialExtension>();
            foreach (var item in models)
            {
                list.Add(Model2Entity(item));
            }
            return list;
        }

        public override Expressionable<MaterialExtension> FilterModel2Where(MaterialFilterModel filterModel)
        {
            var expr = base.FilterModel2Where(filterModel);

            if (!string.IsNullOrEmpty(filterModel._SearchInput))
            {
                expr = expr.And(x => x.Code.Contains(filterModel._SearchInput) || x.MaterialName.Contains(filterModel._SearchInput));
            }
            if (!string.IsNullOrEmpty(filterModel._MaterialName))
            {
                expr = expr.And(x => x.MaterialName == filterModel._MaterialName);
            }
            if (!string.IsNullOrEmpty(filterModel._Code))
            {
                expr = expr.And(x => x.Code == filterModel._Code);
            }
            if (filterModel._MaterialTypeID.HasValue)
            {
                expr = expr.And(x => x.MaterialTypeID == filterModel._MaterialTypeID.Value);
            }
            if (filterModel._MaterialFeatureID.HasValue)
            {
                expr = expr.And(x => x.MaterialFeatureID == filterModel._MaterialFeatureID.Value);
            }
            if (filterModel._MaterialCostID.HasValue)
            {
                expr = expr.And(x => x.MaterialCostID == filterModel._MaterialCostID.Value);
            }
            if (filterModel._MaterialNatureID.HasValue)
            {
                expr = expr.And(x => x.MaterialNatureID == filterModel._MaterialNatureID.Value);
            }
            if (filterModel._ImportantLevel.HasValue)
            {
                expr = expr.And(x => x.ImportantLevel == filterModel._ImportantLevel.Value);
            }
            if (filterModel._IsDelete.HasValue)
            {
                expr = expr.And(x => x.IsDelete == filterModel._IsDelete.Value);
            }

            return expr;
        }
    }
```
```c#
public interface IMaterialRepository : IRepository<MaterialExtension>
    {

    }

    public class MaterialRepository : BaseRepository<MaterialExtension>, IMaterialRepository
    {
        public MaterialRepository(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, httpContextAccessor)
        {

        }

        public override ISugarQueryable<MaterialExtension> GetSearch(Expressionable<MaterialExtension> expressionable, string orderby)
        {
            var query = DB.Queryable<Material, MaterialDetail, MaterialCost, MaterialNature, MaterialFeature, MaterialType>((x, b, c, d, e, f) => new JoinQueryInfos
                   (
                       JoinType.Left, x.MaterialID == b.MaterialID,
                       JoinType.Left, x.MaterialCostID == c.CostID,
                       JoinType.Left, x.MaterialNatureID == d.NatureID,
                       JoinType.Left, x.MaterialFeatureID == e.FeatureID,
                       JoinType.Left, x.MaterialTypeID == f.MaterialTypeID
                   )).Select<MaterialExtension>();

            return _query.GetSearch(query, orderby);
        }

        public override ISugarQueryable<MaterialExtension> GetPage(Expressionable<MaterialExtension> expressionable, int pageIndex, int pageSize, string orderby)
        {
            var query = DB.Queryable<Material, MaterialDetail, MaterialCost, MaterialNature, MaterialFeature, MaterialType>((x, b, c, d, e, f) => new JoinQueryInfos
                   (
                       JoinType.Left, x.MaterialID == b.MaterialID,
                       JoinType.Left, x.MaterialCostID == c.CostID,
                       JoinType.Left, x.MaterialNatureID == d.NatureID,
                       JoinType.Left, x.MaterialFeatureID == e.FeatureID,
                       JoinType.Left, x.MaterialTypeID == f.MaterialTypeID
                   )).Select<MaterialExtension>();

            return _query.GetPage(query, pageIndex, pageSize, orderby);
        }

        public override MaterialExtension GetModel(Expressionable<MaterialExtension> expressionable, string orderBy = null)
        {
            var query = DB.Queryable<Material, MaterialDetail, MaterialCost, MaterialNature, MaterialFeature, MaterialType>((x, b, c, d, e, f) => new JoinQueryInfos
                   (
                       JoinType.Left, x.MaterialID == b.MaterialID,
                       JoinType.Left, x.MaterialCostID == c.CostID,
                       JoinType.Left, x.MaterialNatureID == d.NatureID,
                       JoinType.Left, x.MaterialFeatureID == e.FeatureID,
                       JoinType.Left, x.MaterialTypeID == f.MaterialTypeID
                   )).Select<MaterialExtension>();

            return _query.GetModel(query, orderBy);
        }

        public override MaterialExtension GetModelByPrimaryKey(int id)
        {
            var query = DB.Queryable<Material, MaterialDetail, MaterialCost, MaterialNature, MaterialFeature, MaterialType>((x, b, c, d, e, f) => new JoinQueryInfos
                   (
                       JoinType.Left, x.MaterialID == b.MaterialID,
                       JoinType.Left, x.MaterialCostID == c.CostID,
                       JoinType.Left, x.MaterialNatureID == d.NatureID,
                       JoinType.Left, x.MaterialFeatureID == e.FeatureID,
                       JoinType.Left, x.MaterialTypeID == f.MaterialTypeID
                   )).Select<MaterialExtension>();

            return query.InSingle(id);
        }
    }
```

如果你需要找到底层的一些实现来帮助你进行复杂业务的开发，那么你可以在`Base`文件夹中的`BaseQuery.cs`或者`UnitOfWork.cs`中找到可能对你有帮助的内容。

### **<a name="35" id="35">3.5 API</a>**

这是框架中对于北向网关的实现，从本质上来说，这是一个`WebAPI`项目，我们采用基本的WebAPI中间件来实现它。当然了，作为一个应用的程序启动所在，我们需要在启动时做注入一些中间件来实现整个应用程序的有序启动。

当然，为了使得框架更加的完整和规范，我们也会在WebAPI中采用依赖注入和控制翻转的思想来规范化对象实例化这一动作，但这部分内容很简单，框架本身已经提供了足够的实践。

另外一点比较特别的地方在于，由于内部的业务需求的特点，我们集成了应用的统一认证及授权服务，你可以在开发过程中，在`StartUp.cs`中轻松的取消这部分限定。当然了，如果你取消了限定，那么就无法在开发过程中尝试从请求上下文中获取当前用户信息，我们现在使用`OAuth2.0`协议的标准来进行身份认证及授权。

我们并没有使用.NETCore默认提供的依赖注入容器，而是采用了`Autofac`来作为依赖注入的实现，因为我们对`Autofac`更熟悉并且`Autofac`可以更好的支持我们的控制反转动作。

由于这是一个轻量级的开发框架，所以我们不会默认注入任何上层网关和服务治理相关的组件。对于微服务的支持改造，我们将在微服务的框架中进行详细的说明。

我们需要对核心的一部分代码进行一些基本的解释，以方便开发人员在开发过程中可以灵活的针对中间件进行一些配置。
*需要说明的是，目前的服务中间件并不完善，我们还会持续的对中间件进行完善，因为.NETCores使用的是标准的洋葱圈模型，所以中间件的升级并不会影响到用户代码和相关业务逻辑。*

```c#
public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddControllers().AddControllersAsServices();

            services.AddAuthorization();//加入授权管道中间件
            /**
             * 加入认证管道中间件
             * 参数是指定进行身份认证的地址，因为身份认证服务是单独的服务
             * ApiName是Scope中的包含，来判断当前的Token是否可以访问这个服务
             */
            services.AddAuthentication("Bearer").AddIdentityServerAuthentication(options =>
            {
                options.Authority = "http://192.168.0.101:4090";
                options.RequireHttpsMetadata = false;
                options.ApiName = "MaterialService";
            });

            #region 开启Gzip压缩，并指定速度最快的策略
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });
            #endregion
        }

        // 除了身份认证之外，这里面并没有默认添加任务侵入式的中间件，原则上你是不需要删减这部分代码的
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseResponseCompression();
        }

        //因为采用了Autofac作为依赖注入的容器，所以需要通过Autofac提供的容器入口来进行依赖注入的工厂设置。
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<BaseUnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();


        }
```
---

## **<a name="4" id="4">4.使用规范</a>**

我们希望这些规范可以帮助你进行开发，而非仅仅为了限定。
+ 通常我们习惯使用Pascal规则作为命名规范
+ 不要尝试在Controller层中处理任何业务逻辑，按照依赖注入的核心思想来说，API负责的只是网关功能。
+ 对于5张表以上的联合查询，我们建议使用两次查询并在Logic中对数据进行再组装，而非一次性生成过多表的联合查询。但如果你查询的结果字段并不多，可以根据实际情况进行决断，但永远不要超过8张表在一次查询中连接或聚合。
+ 尽量使用ORM提供的功能来实现你的数据业务，而非借助数据库本身的能力，因为这会加大迁移和维护的难度。
+ 对于领域模型内的对象（`Logic`、`Mappinng`、`Repository`)，永远不要在代码中使用`new`关键字来进行实例化，你应该通过依赖注入来获取对应的实例。
+ 不要随意破坏框架各个项目之间的依赖关系。
+ 如果你需要编写抽象代码，不要直接在业务名称命名的文件夹内进行编写，你应该在与同事沟通后决定如何隔离相关文件与确定引用关系。

---

## **<a name="5" id="5">5.框架规划</a>**

这个框架目前并没有给出具体的版本号，但是根据经验我们已经可以使用它来进行常规的业务开发。我们将会根据实际工作中遇到的情况来决断框架后续的迭代计划。目前已知会逐步加入的能力包括：
+ 对于应用程序访问日志的自动记录功能
+ 对于全局应用异常的拦截器
+ 自动化测试框架
+ 对于API的HTTP路由网管的自动化文档管理(`Swagger`)
