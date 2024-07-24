'use server'

import { Event, SearchEventOption } from '@/types/event.types'
import { Card, CardContent, CardFooter, CardHeader, CardTitle } from '@/components/ui/card'
import { Tabs, TabsContent } from '@/components/ui/tabs'
import AppEventTable from '@/app/admin/events/_components/table-events'
import EventTableHeader from '@/app/admin/events/_components/table-header-feature'
import AppPagination from '@/components/table-pagination'
import { searchEventAction } from '@/actions/event.actions'
import { deleteAuthCookie, handleAuthError } from '@/actions/auth.actions'
import { redirect } from 'next/navigation'
import { apiErrorHandler } from '@/lib/utils'

type Props = {
  searchParams: SearchEventOption
}

export default async function EventPage({ searchParams }: Props) {
  let events: Event[] = []
  let totalPages = 0

  try {
    const response = await searchEventAction(searchParams)
    events = response.data?.data ?? []
    totalPages = response.data?.totalPages ?? 0
  } catch (error: any) {
    console.error('Failed to fetch eventsgggggggg:', error.message)
    const message = error.message as string
    if (message.startsWith('401|')) {
      redirect('/api/auth/test')
    }
    apiErrorHandler(error)
  }

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
                totalPages={totalPages}
              />
            ) : null}
          </CardFooter>
        </Card>
      </TabsContent>
    </Tabs>
  )
}
