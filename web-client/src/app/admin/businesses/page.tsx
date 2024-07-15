import { Card, CardContent, CardFooter, CardHeader, CardTitle } from '@/components/ui/card'
import { Tabs, TabsContent } from '@/components/ui/tabs'
import AppPagination from '@/components/table-pagination'
import { BusinessResponse, SearchBusinessOption } from '@/types/business.types'
import { searchBusinessAction } from '@/actions/business.action'
import AppBusinessTable from '@/app/admin/businesses/_components/table-business'
import BusinessTableHeader from '@/app/admin/businesses/_components/table-header-feature'

type Props = {
  searchParams: SearchBusinessOption
}

export default async function BusinessPage({ searchParams }: Props) {
  const response = await searchBusinessAction(searchParams)
  const businesses: BusinessResponse[] = response.data?.data ?? []
  return (
    <Tabs defaultValue='all'>
      <TabsContent value='all'>
        <Card x-chunk='dashboard-06-chunk-0'>
          <CardHeader>
            <CardTitle className='text-center font-bold text-3xl'>Quản lý doanh nghiệp</CardTitle>
          </CardHeader>
          <CardContent>
            <BusinessTableHeader searchOptions={searchParams} />
            {businesses != null && businesses.length > 0 ? (
              <AppBusinessTable data={businesses} />
            ) : (
              <div className='flex flex-col items-center gap-1 text-center mt-10'>
                <h3 className='text-2xl font-bold tracking-tight'>Không tìm thấy doanh nghiệp nào tương ứng</h3>
                <p className='text-sm text-muted-foreground'>Bạn có thể tạo mới doanh nghiệp ngay lập tức</p>
              </div>
            )}
          </CardContent>
          <CardFooter>
            {businesses != null && businesses.length > 0 ? (
              <AppPagination
                currentPage={searchParams.pageIndex ? Number(searchParams.pageIndex) : 1}
                totalPages={response.data.totalPages}
              />
            ) : null}
          </CardFooter>
        </Card>
      </TabsContent>
    </Tabs>
  )
}
