import { Event, SearchEventOption } from '@/types/event.types'
import { Card, CardContent, CardFooter, CardHeader, CardTitle } from '@/components/ui/card'
import { Tabs, TabsContent } from '@/components/ui/tabs'
import AppEventTable from '@/app/admin/events/_components/table-events'
import EventTableHeader from '@/app/admin/events/_components/table-header-feature'
import AppPagination from '@/components/table-pagination'
import { searchEventAction } from '@/actions/event.actions'

type Props = {
  searchParams: SearchEventOption
}

export default async function EventPage({ searchParams }: Props) {
  const response = await searchEventAction(searchParams)
  const events: Event[] = response.data?.data ?? []
  return (
    <Tabs defaultValue='all'>
      <TabsContent value='all'>
        <Card x-chunk='dashboard-06-chunk-0'>
          <CardHeader>
            <CardTitle className='text-center font-bold text-3xl'>Quản lý sự kiện</CardTitle>
          </CardHeader>
          <CardContent>
            <EventTableHeader searchOptions={searchParams} />
            {events != null && events.length > 0 ? (
              <AppEventTable data={events} />
            ) : (
              <div className='flex flex-col items-center gap-1 text-center mt-10'>
                <h3 className='text-2xl font-bold tracking-tight'>Không tìm thấy sự kiện nào tương ứng</h3>
                <p className='text-sm text-muted-foreground'>Bạn có thể tạo mới sự kiện ngay lập tức</p>
              </div>
            )}
          </CardContent>
          <CardFooter>
            {events != null && events.length > 0 ? (
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
