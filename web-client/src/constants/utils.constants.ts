export type BreadcrumbType = {
  url: string;
  display: string;
  value: string;
};

export const BreadcrumbValue = new Map<string, BreadcrumbType>([
  [
    "/admin/dashboard",
    {
      url: "/admin/dashboard",
      display: "Quản lý thống kê",
      value: "Toàn bộ thống kê",
    },
  ],
  [
    "/admin/users",
    {
      url: "/admin/users",
      display: "Quản lý nhân viên",
      value: "Toàn bộ nhân viên",
    },
  ],
  [
    "/admin/products",
    {
      url: "/admin/products",
      display: "Quản lý sản phẩm",
      value: "Toàn bộ sản phẩm",
    },
  ],
]);
