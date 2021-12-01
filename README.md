# gRPC Service Demo

![](https://i.imgur.com/tcuxrOd.png)

---

## 架構說明

* **WebAPI Module**
專門處理 **外部** 請求，根據請求與各個對應 **DomainService** 溝通並完成請求作業。
* **DomainService** 
    * **Interface**
    專門處理來自 **Module** 的請求，為 **DomainService** 接口。
    * **Core**
    專門處理該 **DomainService** 的核心商業邏輯。
    * **Database**
    存放該 **DomainService** 的資料。
* **Common**
各專案 **共用元件** 存放處。

---

## 資料夾結構

* **build**
    * **sql**
        * **init.sql** 為 Docker-Compose 啟動時， MSSQL 初始化執行的腳本
    * **Docker-Compose.yml** 用來建置整體應用程式的架構
    * **WebApplication.Dockerfile** 用來建置 GrpcDemo.WebApplication 的 Image
    * **DomainService.Dockerfile** 用來建置 GrpcDemo.DomainService 的 Image
* **src**
    * **GrpcDemo.Common** 放置 Response Wrapper 與狀態碼
    * **GrpcDemo.WebApplication**
        * **Controllers** 放置處理來自外部 API 的接口物件
        * **Parameters** 放置外部傳進來的參數物件
        * **ViewModels** 放置內部回傳的參數物件
        * **Utilities**
            * **Mappers** 放置參數映射的 Profile 物件
            * **Middlewares** 放置中介層的物件
    * **GrpcDemo.Message** 放置 gRPC 規格的定義 proto 檔
    * **GrpcDemo.DomainService**
        * **Implements** 放置處理來自 Module 的接口物件
        * **Utilities**
            * **Mappers** 放置參數映射的 Profile 物件
            * **Interceptors** 放置 gRPC 攔截器的物件
    * **GrpcDemo.DomainService.Core**
        * **DTOs** 放置 DTO 資料傳輸物件
        * **Entities** 放置 Entity 資料庫的第一層轉型物件
        * **Interfaces** 放置 Service、Repository 定義的介面
        * **Misc** 放置雜項(DML 操作的回傳物件)
        * **Services** 放置商業邏輯層的實作
        * **Repositories** 放置資料存取層的實作
        * **Utilities**
            * **Helpers** 放置資料庫連線的物件
    * **GrpcDemo.Test** 放置單元測試的類別

---

## 工具
* **CI/CD**
    * Docker Container
* **API Testing**
    * Swagger
* **參數驗證**
    * FluentValidation(待補)
* **分散式效能追蹤**
    * Jaeger(待補)
* **單元測試**
    * FluentAssertion
    * NSubstitute
    * AutoFixture
* **參數映射**
    * AutoMapper
* **ORM**
    * Dapper

---

## 啟動

第一步：將專案 restore 並確認編譯成功

第二步：於資料夾 build 階層執行指令 `docker-compose up -d`

![](https://i.imgur.com/KLHd2a3.jpg)

執行成功畫面

![](https://i.imgur.com/6t1kqEC.png)

第三步：瀏覽網址 `http://localhost:5555/swagger/index.html`

看到此網頁畫面即可進行操作

![](https://i.imgur.com/ztzLtbB.png)


